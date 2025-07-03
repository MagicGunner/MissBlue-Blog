using System.Linq.Expressions;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface IArticleRepository : IBaseRepositories<Article> {
    Task<List<Article>> FullQueryAsync(
        Expression<Func<Article, bool>>?   whereExpression   = null,
        Expression<Func<Article, object>>? orderByExpression = null,
        OrderByType                        orderByType       = OrderByType.Asc,
        int?                               pageNum           = null,
        int?                               pageSize          = null,
        RefAsync<int>?                     totalCount        = null,
        int?                               take              = null,
        bool                               isRandom          = false
    );

    Task<string> GetCategoryName(Article article);
}