using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;

namespace Backend.Infrastructure.Repository;

public class BannersRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Banners>(unitOfWorkManage), IBannersRepository {
    public async Task<List<string>>  GetBanners()     => await Db.Queryable<Banners>().OrderBy(b => b.SortOrder).Select(b => b.Path).ToListAsync();
    public async Task<List<Banners>> BackGetBanners() => await Db.Queryable<Banners>().OrderBy(b => b.SortOrder).ToListAsync();

    public async Task<Banners?> RemoveBannerById(long id) => await Db.Queryable<Banners>().InSingleAsync(id);

    public async Task<(bool success, string msg)> UpdateSortOrder(List<Banners> banners) {
        var result = await Db.Ado.UseTranAsync(async () => {
                                                   // 1. 删除所有 banner
                                                   await Db.Deleteable<Banners>().ExecuteCommandAsync();

                                                   // 2. 重新设置排序并插入
                                                   for (int i = 0; i < banners.Count; i++) {
                                                       banners[i].SortOrder = i + 1;
                                                       banners[i].CreateTime = DateTime.Now; // 若数据库不自动生成
                                                   }

                                                   await Db.Insertable(banners).ExecuteCommandAsync();
                                               });
        return result.IsSuccess
                   ? (true, "排序更新成功")
                   : (false, "排序更新失败" + result.ErrorMessage);
    }
}