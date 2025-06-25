using System.Linq.Expressions;
using AutoMapper;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Domain;
using Backend.Domain.Entity;
using SqlSugar;

namespace Backend.Application;

public class BaseServices<TEntity, TVo>(IMapper mapper, IBaseRepositories<TEntity> baseRepositories) : IBaseServices<TEntity, TVo>
    where TEntity : class, new() {
    public ISqlSugarClient Db => baseRepositories.Db;

    public async Task<long> AddAsync(TEntity entity) {
        return await baseRepositories.Add(entity);
    }

    public async Task<ResponseResult<object>> DeleteAsync(TEntity entity) =>
        await baseRepositories.Delete(entity)
            ? ResponseResult<object>.Success(null, "删除成功")
            : ResponseResult<object>.Failure(msg: "删除失败");

    public async Task<ResponseResult<object>> DeleteByIdsAsync(List<long> ids) =>
        await baseRepositories.DeleteByIds(ids)
            ? ResponseResult<object>.Success(null, "删除成功")
            : ResponseResult<object>.Failure(msg: "删除失败");

    public async Task<ResponseResult<object>> UpdateAsync(TEntity entity) =>
        await baseRepositories.Update(entity)
            ? ResponseResult<object>.Success(null, "修改成功")
            : ResponseResult<object>.Failure(msg: "修改失败");

    public async Task<ResponseResult<object>> UpdateAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updateExpression) =>
        await baseRepositories.Update(whereExpression, updateExpression)
            ? ResponseResult<object>.Success(null, "修改成功")
            : ResponseResult<object>.Failure(msg: "修改失败");

    public async Task<List<TVo>> QueryAsync(Expression<Func<TEntity, bool>>? whereExpression = null) => mapper.Map<List<TVo>>(await baseRepositories.Query(whereExpression));
}