using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;
using SqlSugar;

namespace Backend.Infrastructure.Repository;

public class LookupRepository<TEntity>(IUnitOfWorkManage unitOfWorkManage) : ILookupRepository<TEntity> where TEntity : class, new() {
    private readonly SqlSugarScope Db = us

    public async Task<Dictionary<long, TEntity>> GetEntityDic(List<long> entityIds) => await
}