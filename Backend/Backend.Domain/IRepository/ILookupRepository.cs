namespace Backend.Domain.IRepository;

public interface ILookupRepository<TEntity> where TEntity : class {
    Task<Dictionary<long, TEntity>> GetEntityDic(List<long> entityIds);
}