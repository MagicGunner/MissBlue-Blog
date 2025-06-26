using System.Linq.Expressions;
using AutoMapper;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Domain;
using Backend.Domain.Entity;
using SqlSugar;

namespace Backend.Application;

public class BaseServices<TEntity>(IMapper mapper, IBaseRepositories<TEntity> baseRepositories) : IBaseServices<TEntity>
    where TEntity : class, new() {
    public ISqlSugarClient Db => baseRepositories.Db;

    public async Task<long> AddAsync(TEntity entity) {
        return await baseRepositories.Add(entity);
    }

    public async Task<bool> DeleteAsync(TEntity         entity) => await baseRepositories.Delete(entity);
    public async Task<bool> DeleteByIdsAsync(List<long> ids)    => await baseRepositories.DeleteByIds(ids);
    public async Task<bool> UpdateAsync(TEntity         entity) => await baseRepositories.Update(entity);

    public async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updateExpression) =>
        await baseRepositories.Update(whereExpression, updateExpression);

    public async Task<List<TVo>> QueryAsync<TVo>(Expression<Func<TEntity, bool>>? whereExpression = null) => mapper.Map<List<TVo>>(await baseRepositories.Query(whereExpression));
}