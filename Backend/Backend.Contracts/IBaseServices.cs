using System.Linq.Expressions;
using Backend.Common.Results;
using SqlSugar;

namespace Backend.Contracts;

public interface IBaseServices<TEntity> where TEntity : class {
    ISqlSugarClient Db { get; }

    #region 增(Add)

    /// <summary>
    /// 写入实体数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<long> AddAsync(TEntity entity);

    #endregion

    #region 删(Delete)

    Task<bool> DeleteAsync(TEntity         entity);
    Task<bool> DeleteByIdsAsync(List<long> ids);

    #endregion

    #region 改(Update)

    Task<bool> UpdateAsync(TEntity entity);

    Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updateExpression);

    #endregion

    #region 查(Query)

    /// <summary>
    /// 查询后台数据
    /// </summary>
    /// <param name="whereExpression">默认为空，为空时查询所有</param>
    /// <returns></returns>
    Task<List<TVo>> QueryAsync<TVo>(Expression<Func<TEntity, bool>>? whereExpression = null);

    #endregion
}