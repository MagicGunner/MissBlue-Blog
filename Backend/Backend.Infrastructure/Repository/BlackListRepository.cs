using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;

namespace Backend.Infrastructure.Repository;

public class BlackListRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<BlackList>(unitOfWorkManage), IBlackListRepository {
    public async Task<long?> GetIdByIp(string ip) {
        var sql = "SELECT id FROM t_black_list WHERE JSON_UNQUOTE(JSON_EXTRACT(ip_info, '$.createIp')) = @ip LIMIT 1";
        var result = await Db.Ado.SqlQuerySingleAsync<long?>(sql, new { ip });
        return result == 0 ? null : result;
    }
}