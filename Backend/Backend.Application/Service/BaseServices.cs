using System.Linq.Expressions;
using AutoMapper;
using Backend.Contracts.IService;
using Backend.Domain;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using SqlSugar;

namespace Backend.Application.Service;

public class BaseServices<TEntity>(IMapper mapper, IBaseRepositories<TEntity> baseRepositories) : IBaseServices<TEntity>
    where TEntity : RootEntity, new() {
    public ISqlSugarClient Db => baseRepositories.Db;

    public async Task<long> Add(TEntity entity) {
        return await baseRepositories.Add(entity);
    }

    public async         Task<bool> Delete(TEntity         entity) => await baseRepositories.Delete(entity);
    public async         Task<bool> DeleteByIds(List<long> ids)    => await baseRepositories.DeleteByIds(ids);
    public virtual async Task<bool> Update(TEntity         entity) => await baseRepositories.Update(entity);

    public async Task<bool> Update(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TEntity>> updateExpression) =>
        await baseRepositories.Update(whereExpression, updateExpression);

    public async Task<List<TVo>> Query<TVo>(Expression<Func<TEntity, bool>>? whereExpression = null) => mapper.Map<List<TVo>>(await baseRepositories.Query(whereExpression));

    public async Task<List<TVo>> QueryWithMulti<TVo>(Func<ISugarQueryable<TEntity>, ISugarQueryable<TEntity>>? buildQuery = null) =>
        mapper.Map<List<TVo>>(await baseRepositories.QueryWithMulti(buildQuery));
}