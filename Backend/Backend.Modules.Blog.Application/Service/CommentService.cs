using AutoMapper;
using Backend.Contracts;
using Backend.Contracts.IService;
using Backend.Domain;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class CommentService(IMapper mapper, IBaseRepositories<Comment> baseRepositories, IBaseServices<Comment> baseServices) : ICommentService {
    public Task<long> Add(UserCommentDTO userCommentDto) {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteByIds(List<long> ids) {
        throw new NotImplementedException();
    }

    public Task<bool> Update(UserCommentDTO userCommentDto) {
        throw new NotImplementedException();
    }

    public Task<List<ArticleCommentVO>> ListAll() {
        throw new NotImplementedException();
    }

    public async Task<PageVO<List<ArticleCommentVO>>> GetComment(int type, int typeId, int pageNum, int pageSize) {
        // 查询父评论
        var parentComments = await baseServices.QueryWithMulti<ArticleCommentVO>(query =>
                                                                                     query.Where(x => x.Type == type && x.TypeId == typeId && x.IsCheck == 1 && x.ParentId == null)
                                                                                          .OrderBy(x => x.CreateTime, OrderByType.Desc)
                                                                                );

        // 查询子评论
        var childComments = await baseServices.QueryWithMulti<ArticleCommentVO>(query =>
                                                                                    query.Where(x => x.Type == type && x.TypeId == typeId && x.IsCheck == 1 && x.ParentId != null)
                                                                                         .OrderBy(x => x.CreateTime, OrderByType.Desc)
                                                                               );

        // 内存组装父子评论
        foreach (var parent in parentComments) {
            parent.ChildComment = childComments.Where(c => c.ParentId == parent.Id).ToList();
            parent.ChildCommentCount = parent.ChildComment.Count;

            parent.ParentCommentCount = parentComments.Count;
        }

        return new PageVO<List<ArticleCommentVO>> {
                                                      Page = parentComments,
                                                      Total = parentComments.Count
                                                  };
    }

    public Task<List<CommentListVO>> Search(SearchCommentDTO searchCommentDto) {
        throw new NotImplementedException();
    }
}