using System.Linq.Expressions;
using System.Reflection;
using Backend.Common.Core;
using Backend.Domain.Entity;
using Backend.Domain.Entity.Tenants;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;
using SqlSugar;
using MultiTenantAttribute = Backend.Infrastructure.Attributes.MultiTenantAttribute;

namespace Backend.Infrastructure.Repository;

public class BaseRepositories<TEntity>(IUnitOfWorkManage unitOfWorkManage) : IBaseRepositories<TEntity>
    where TEntity : RootEntity, new() {
    private readonly SqlSugarScope     _dbBase           = unitOfWorkManage.GetDbClient();
    private readonly IUnitOfWorkManage _unitOfWorkManage = unitOfWorkManage;
    public           ISqlSugarClient   Db => _db;

    private ISqlSugarClient _db {
        get {
            ISqlSugarClient db = _dbBase;

            //修改使用 model备注字段作为切换数据库条件，使用sqlsugar TenantAttribute存放数据库ConnId
            //参考 https://www.donet5.com/Home/Doc?typeId=2246
            var tenantAttr = typeof(TEntity).GetCustomAttribute<TenantAttribute>();
            if (tenantAttr != null) {
                //统一处理 configId 小写
                db = _dbBase.GetConnectionScope(tenantAttr.configId.ToString().ToLower());
                return db;
            }

            //多租户
            var mta = typeof(TEntity).GetCustomAttribute<MultiTenantAttribute>();
            if (mta is { TenantType: TenantTypeEnum.Db }) {
                //获取租户信息 租户信息可以提前缓存下来 
                if (App.User is { TenantId: > 0 }) {
                    //.WithCache()
                    var tenant = db.Queryable<SysTenant>().WithCache().Where(s => s.Id == App.User.TenantId).First();
                    if (tenant != null) {
                        var iTenant = db.AsTenant();
                        if (!iTenant.IsAnyConnection(tenant.ConfigId)) {
                            iTenant.AddConnection(tenant.GetConnectionConfig());
                        }

                        return iTenant.GetConnectionScope(tenant.ConfigId);
                    }
                }
            }

            return db;
        }
    }


    #region 增(Add)

    public async Task<bool> InsertOrUpdate(TEntity entity) => await Db.Storageable(entity).ExecuteCommandAsync() > 0;

    /// <summary>
    /// 写入实体数据
    /// </summary>
    /// <param name="entity">博文实体类</param>
    /// <returns></returns>
    public async Task<long> Add(TEntity entity) {
        var insert = _db.Insertable(entity);
        return await insert.ExecuteReturnBigIdentityAsync();
    }

    public Task<int> Add(List<TEntity> entities) {
        return _db.Insertable(entities).ExecuteCommandAsync();
    }

    /// <summary>
    /// 写入实体数据
    /// </summary>
    /// <param name="entity">数据实体</param>
    /// <returns></returns>
    public async Task<List<long>> AddSplit(TEntity entity) {
        var insert = _db.Insertable(entity).SplitTable();
        //插入并返回雪花ID并且自动赋值ID　
        return await insert.ExecuteReturnSnowflakeIdListAsync();
    }

    #endregion

    #region 删(Delete)

    /// <summary>
    /// 根据主键ID删除，默认不开启事务，如果需要开启事务，请在各自仓储中重写
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> DeleteByIds(List<long> ids) => await _db.Deleteable<TEntity>().In(ids).ExecuteCommandAsync() > 0;

    /// <summary>
    /// 根据实体删除（根据主键）
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> Delete(TEntity entity) => await _db.Deleteable(entity).ExecuteCommandAsync() > 0;

    /// <summary>
    /// 根据条件批量删除
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public async Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression) => await _db.Deleteable<TEntity>().Where(whereExpression).ExecuteCommandAsync() > 0;

    #endregion

    #region 改(Update)

    /// <summary>
    /// 根据实体更新（根据主键更新）
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<bool> Update(TEntity entity) => await _db.Updateable(entity).ExecuteCommandAsync() > 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="whereExpression">指定表的列</param>
    /// <param name="updateExpression">指定属性如何修改</param>
    /// <returns></returns>
    public async Task<bool> Update(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updateExpression) {
        return await Db.Updateable<TEntity>()
                       .SetColumns(updateExpression)
                       .Where(whereExpression)
                       .ExecuteCommandAsync() > 0;
    }

    #endregion

    #region 查(Query)

    public async Task<List<TEntity>> GetByIds(List<long> ids) {
        return await Db.Queryable<TEntity>().In(entity => entity.Id, ids).ToListAsync();
    }

    public async Task<TEntity?> GetById(long id) {
        return await Db.Queryable<TEntity>().Where(t => t.Id == id).SingleAsync();
    }

    public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>>? expression = null) {
        // await Console.Out.WriteLineAsync(Db.GetHashCode().ToString());
        return await _db.Queryable<TEntity>().WhereIF(expression != null, expression).ToListAsync();
    }

    public async Task<List<TEntity>> QueryWithMulti(Func<ISugarQueryable<TEntity>, ISugarQueryable<TEntity>>? buildQuery = null) {
        var query = _db.Queryable<TEntity>();

        if (buildQuery != null) {
            query = buildQuery(query);
        }

        return await query.ToListAsync();
    }

    public async Task<List<TEntity>> QueryWithCache(Expression<Func<TEntity, bool>>? whereExpression = null) {
        return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).WithCache().ToListAsync();
    }

    /// <summary>
    /// 分表查询
    /// </summary>
    /// <param name="whereExpression">条件表达式</param>
    /// <param name="orderByFields">排序字段，如name asc,age desc</param>
    /// <returns></returns>
    public async Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>>? whereExpression, string? orderByFields = null) {
        return await _db.Queryable<TEntity>()
                        .SplitTable()
                        .OrderByIF(!string.IsNullOrEmpty(orderByFields), orderByFields)
                        .WhereIF(whereExpression != null, whereExpression)
                        .ToListAsync();
    }

    public async Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression,
                                                                   Expression<Func<T, T2, T3, TResult>>  selectExpression,
                                                                   Expression<Func<T, T2, T3, bool>>?    whereLambda = null) where T : class, new() {
        if (whereLambda == null) {
            return await _db.Queryable(joinExpression).Select(selectExpression).ToListAsync();
        }

        return await _db.Queryable(joinExpression).Where(whereLambda).Select(selectExpression).ToListAsync();
    }

    public async Task<Dictionary<long, TResult>> GetEntityDic<TResult>(List<long> entityIds) where TResult : RootEntity =>
        (await _db.Queryable<TResult>().In(entity => entity.Id, entityIds).ToListAsync()).ToDictionary(entity => entity.Id, entity => entity);

    #endregion
}