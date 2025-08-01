﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Backend.Common;

public class AppSettings {
    public static IConfiguration Configuration { get; set; }
    static        string         contentPath   { get; set; }

    public AppSettings(string contentPath) {
        var path = "appsettings.json";

        //如果你把配置文件 是 根据环境变量来分开了，可以这样写
        //Path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";

        Configuration = new ConfigurationBuilder()
                       .SetBasePath(contentPath)
                       .Add(new JsonConfigurationSource {
                                                            Path = path,
                                                            Optional = false,
                                                            ReloadOnChange = true
                                                        }) //这样的话，可以直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
                       .Build();
    }

    public AppSettings(IConfiguration configuration) {
        Configuration = configuration;
    }

    /// <summary>
    /// 封装要操作的字符
    /// </summary>
    /// <param name="sections">节点配置</param>
    /// <returns></returns>
    public static string App(params string[] sections) {
        try {
            if (sections.Length != 0) {
                return Configuration[string.Join(":", sections)];
            }
        } catch (Exception) {
            // ignored
        }

        return "";
    }

    /// <summary>
    /// 递归获取配置信息数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sections"></param>
    /// <returns></returns>
    public static List<T> App<T>(params string[] sections) {
        var list = new List<T>();
        // 引用 Microsoft.Extensions.Configuration.Binder 包
        Configuration.Bind(string.Join(":", sections), list);
        return list;
    }


    /// <summary>
    /// 根据路径  configuration["App:Name"];
    /// </summary>
    /// <param name="sectionsPath"></param>
    /// <returns></returns>
    public static string GetValue(string sectionsPath) {
        try {
            return Configuration[sectionsPath];
        } catch (Exception) {
        }

        return "";
    }
}