using System.Linq.Expressions;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;
using SqlSugar;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class ArticleRepository(IUnitOfWorkManage unitOfWorkManage, ICategoryRepository categoryRepository) : BaseRepositories<Article>(unitOfWorkManage), IArticleRepository {
    public async Task<List<Article>> FullQueryAsync(
        Expression<Func<Article, bool>>?   whereExpression   = null,
        Expression<Func<Article, object>>? orderByExpression = null,
        OrderByType                        orderByType       = OrderByType.Asc,
        int?                               pageNum           = null,
        int?                               pageSize          = null,
        RefAsync<int>?                     totalCount        = null,
        int?                               take              = null,
        bool                               isRandom          = false) {
        var query = Db.Queryable<Article>(); // _db 为 ISqlSugarClient 实例

        if (whereExpression != null)
            query = query.Where(whereExpression);

        if (isRandom) {
            query = query.OrderBy(SqlFunc.GetRandom());
        } else if (orderByExpression != null) {
            query = query.OrderBy(orderByExpression, orderByType);
        }

        if (pageNum.HasValue && pageSize.HasValue)
            return await query.ToPageListAsync(pageNum.Value, pageSize.Value, totalCount);

        if (take.HasValue) {
            query = query.Take(take.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<string> GetCategoryName(Article article) => await categoryRepository.GetNameByIdAsync(article.CategoryId);
}