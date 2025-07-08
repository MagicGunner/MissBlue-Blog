using System.Linq.Expressions;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface IArticleRepository : IBaseRepositories<Article> {
    Task<List<Article>> FullQuery(Expression<Func<Article, bool>>?   whereExpression   = null,
                                  Expression<Func<Article, object>>? orderByExpression = null,
                                  OrderByType                        orderByType       = OrderByType.Asc,
                                  int?                               pageNum           = null,
                                  int?                               pageSize          = null,
                                  RefAsync<int>?                     totalCount        = null,
                                  int?                               take              = null,
                                  bool                               isRandom          = false);

    Task<Dictionary<long, string>> GetContentDic(List<long> userIds);
    Task<List<Article>>            ListAll(int              pageNum, int pageSize, RefAsync<int> totalCount);
    Task<List<Article>>            ListComment();
    Task<List<Article>>            ListRandom();
    Task<List<Article>>            ListRelated(long categoryId, long articleId);
    Task<List<Article>>            LisHot();
    Task<Article?>                 GetById(long            id);
    Task<Article?>                 FindPre(long            currentId);
    Task<Article?>                 FindNext(long           currentId);
    Task<long>                     CountByTag(long         tagId);
    Task<List<Article>>            FindByContent(string    keyword);
    Task<List<Article>>            ListCategoryArticle(int type, long typeId);
}