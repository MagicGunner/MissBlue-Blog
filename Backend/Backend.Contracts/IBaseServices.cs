using System.Linq.Expressions;
using Backend.Common.Results;
using SqlSugar;

namespace Backend.Contracts;

public interface IBaseServices<TEntity, TVo> where TEntity : class {
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

    Task<ResponseResult<object>> DeleteAsync(TEntity         entity);
    Task<ResponseResult<object>> DeleteByIdsAsync(List<long> ids);

    #endregion

    #region 改(Update)

    Task<ResponseResult<object>> UpdateAsync(TEntity entity);

    Task<ResponseResult<object>> UpdateAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updateExpression);

    #endregion

    #region 查(Query)

    /// <summary>
    /// 查询后台数据
    /// </summary>
    /// <param name="whereExpression">默认为空，为空时查询所有</param>
    /// <returns></returns>
    Task<List<TVo>> QueryAsync(Expression<Func<TEntity, bool>>? whereExpression = null);

    #endregion
}