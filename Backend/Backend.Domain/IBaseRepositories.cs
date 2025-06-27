using System.Linq.Expressions;
using SqlSugar;

namespace Backend.Domain;

public interface IBaseRepositories<TEntity> where TEntity : class {
    ISqlSugarClient Db { get; }

    #region 查询（Query）

    Task<List<TEntity>> Query(Expression<Func<TEntity, bool>>? expression);

    Task<List<TEntity>> QueryWithMulti(Func<ISugarQueryable<TEntity>, ISugarQueryable<TEntity>>? buildQuery                             = null);
    Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>>                               whereExpression, string? orderByFields = null);
    Task<List<TEntity>> QueryWithCache(Expression<Func<TEntity, bool>>?                          whereExpression = null);

    Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression,
                                                      Expression<Func<T, T2, T3, TResult>>  selectExpression,
                                                      Expression<Func<T, T2, T3, bool>>?    whereLambda = null)
        where T : class, new();

    #endregion

    #region 新增（Add）

    Task<long>       Add(TEntity      entity);
    Task<List<long>> AddSplit(TEntity entity);

    #endregion

    #region 修改（Update）

    /// <summary>
    /// 更新单个实体
    /// </summary>
    Task<bool> Update(TEntity entity);

    /// <summary>
    /// 根据条件批量更新
    /// </summary>
    Task<bool> Update(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updateExpression);

    #endregion

    #region 删除（Delete）

    /// <summary>
    /// 根据ID删除
    /// </summary>
    Task<bool> DeleteByIds(List<long> ids);

    /// <summary>
    /// 根据实体删除
    /// </summary>
    Task<bool> Delete(TEntity entity);

    /// <summary>
    /// 根据条件批量删除
    /// </summary>
    Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression);

    #endregion
}