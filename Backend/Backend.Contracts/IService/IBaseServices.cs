using System.Linq.Expressions;
using SqlSugar;

namespace Backend.Contracts.IService;

public interface IBaseServices<TEntity> where TEntity : class {
    ISqlSugarClient Db { get; }

    #region 增(Add)

    /// <summary>
    /// 写入实体数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<long> Add(TEntity entity);

    #endregion

    #region 删(Delete)

    Task<bool> Delete(TEntity         entity);
    Task<bool> DeleteByIds(List<long> ids);

    #endregion

    #region 改(Update)

    Task<bool> Update(TEntity entity);

    Task<bool> Update(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updateExpression);

    #endregion

    #region 查(Query)

    /// <summary>
    /// 查询后台数据
    /// </summary>
    /// <param name="whereExpression">默认为空，为空时查询所有</param>
    /// <returns></returns>
    Task<List<TVo>> Query<TVo>(Expression<Func<TEntity, bool>>? whereExpression = null);

    /// <summary>
    /// 链式多条件查询
    /// </summary>
    /// <param name="buildQuery"></param>
    /// <returns></returns>
    Task<List<TVo>> QueryWithMulti<TVo>(Func<ISugarQueryable<TEntity>, ISugarQueryable<TEntity>>? buildQuery = null);

    #endregion
}