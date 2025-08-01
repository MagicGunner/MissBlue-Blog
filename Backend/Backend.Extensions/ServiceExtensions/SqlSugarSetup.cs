﻿using System.Text.RegularExpressions;
using Backend.Common;
using Backend.Common.Caches;
using Backend.Common.Core;
using Backend.Common.DB;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Backend.Extensions.ServiceExtensions;

public static class SqlSugarSetup {
    public static void AddSqlSugarSetup(this IServiceCollection services) {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 默认添加主数据库连接
        if (!string.IsNullOrEmpty(AppSettings.App("MainDB"))) {
            MainDb.CurrentDbConnId = AppSettings.App("MainDB");
        }

        BaseDBConfig.MutiConnectionString.allDbs.ForEach(m => {
                                                             var config = new ConnectionConfig {
                                                                                                   ConfigId = m.ConnId.ObjToString().ToLower(),
                                                                                                   ConnectionString = m.Connection,
                                                                                                   DbType = (DbType)m.DbType,
                                                                                                   IsAutoCloseConnection = true,
                                                                                                   MoreSettings = new ConnMoreSettings() {
                                                                                                                      IsAutoRemoveDataCache = true,
                                                                                                                      SqlServerCodeFirstNvarchar = true,
                                                                                                                  },
                                                                                                   // 自定义特性
                                                                                                   ConfigureExternalServices = new ConfigureExternalServices {
                                                                                                                                   DataInfoCacheService = new SqlSugarCacheService(),
                                                                                                                                   // ✅ 开启实体属性与表字段映射下划线支持（如 UserId <-> user_id）
                                                                                                                                   EntityService = (type, column) => {
                                                                                                                                       column.DbColumnName
                                                                                                                                           = UtilMethods.ToUnderLine(column.PropertyName);
                                                                                                                                   }
                                                                                                                               },
                                                                                                   InitKeyType = InitKeyType.Attribute,
                                                                                               };
                                                             if (SqlSugarConst.LogConfigId.ToLower().Equals(m.ConnId.ToLower())) {
                                                                 BaseDBConfig.LogConfig = config;
                                                             } else {
                                                                 BaseDBConfig.ValidConfig.Add(config);
                                                             }

                                                             BaseDBConfig.AllConfigs.Add(config);
                                                         });

        if (BaseDBConfig.LogConfig is null) {
            throw new ApplicationException("未配置Log库连接");
        }

        // SqlSugarScope是线程安全，可使用单例注入
        // 参考：https://www.donet5.com/Home/Doc?typeId=1181
        services.AddSingleton<ISqlSugarClient>(o => {
                                                   return new SqlSugarScope(BaseDBConfig.AllConfigs, db => {
                                                                                                         // 开启字段映射 user_id ←→ UserId
                                                                                                         BaseDBConfig.ValidConfig.ForEach(config => {
                                                                                                             var dbProvider = db.GetConnectionScope((string)config.ConfigId);
                                                                                                             // 打印SQL语句
                                                                                                             dbProvider.Aop.OnLogExecuting = (s, parameters) => {
                                                                                                                 SqlSugarAop.OnLogExecuting(dbProvider, App.User?.Name.ObjToString(),
                                                                                                                     ExtractTableName(s),
                                                                                                                     Enum.GetName(typeof(SugarActionType),
                                                                                                                                  dbProvider.SugarActionType), s, parameters,
                                                                                                                     config);
                                                                                                             };
                                                                                                         });
                                                                                                     });
                                               });
    }

    private static string ExtractTableName(string sql) {
        // 匹配 SQL 语句中的表名的正则表达式
        //string regexPattern = @"\s*(?:UPDATE|DELETE\s+FROM|SELECT\s+\*\s+FROM)\s+(\w+)";
        var regexPattern = @"(?i)(?:FROM|UPDATE|DELETE\s+FROM)\s+`(.+?)`";
        var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
        var match = regex.Match(sql);

        return match.Success
                   ?
                   // 提取匹配到的表名
                   match.Groups[1].Value
                   :
                   // 如果没有匹配到表名，则返回空字符串或者抛出异常等处理
                   string.Empty;
    }
}