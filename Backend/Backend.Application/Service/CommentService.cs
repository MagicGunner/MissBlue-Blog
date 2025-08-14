using AutoMapper;
using Backend.Application.Interface;
using Backend.Common.Const;
using Backend.Common.Enums;
using Backend.Common.Redis;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain.Entity;
using Backend.Domain.Enums;
using Backend.Domain.IRepository;
using SqlSugar;

namespace Backend.Application.Service;

public class CommentService(IMapper                    mapper,
                            IBaseRepositories<Comment> baseRepositories,
                            ICommentRepository         commentRepository,
                            ILikeRepository            likeRepository,
                            ICurrentUser               currentUser,
                            IRedisBasketRepository     redisBasketRepository)
    : BaseServices<Comment>(mapper, baseRepositories), ICommentService {
    private readonly IMapper                    _mapper           = mapper;
    private readonly IBaseRepositories<Comment> _baseRepositories = baseRepositories;

    public async Task<(bool IsSuccess, string? Msg)> AddComment(UserCommentDTO userCommentDto) {
        var comment = _mapper.Map<Comment>(userCommentDto);
        if (currentUser.UserId != null) comment.CommentUserId = currentUser.UserId.Value;
        //todo 判断用是否为第三方登录没有邮箱
        return (await commentRepository.AddComment(comment), string.Empty);
    }

    public async Task<PageVO<List<ArticleCommentVO>>> GetComment(int type, int typeId, int pageNum, int pageSize) {
        // 查询父评论
        var parentComments = await QueryWithMulti<ArticleCommentVO>(query =>
                                                                        query.Where(x => x.Type == type && x.TypeId == typeId && x.IsCheck == 1 && x.ParentId == null)
                                                                             .OrderBy(x => x.CreateTime, OrderByType.Desc)
                                                                   );

        // 查询子评论
        var childComments = await QueryWithMulti<ArticleCommentVO>(query =>
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

    public async Task<List<CommentListVO>> GetBackList(SearchCommentDTO? dto) {
        var comments = await commentRepository.GetBackList(dto?.CommentUserName, dto?.CommentContent, dto?.Type, dto?.IsCheck);
        var userDic = await _baseRepositories.GetEntityDic<User>(comments.Select(c => c.CommentUserId).ToList());
        return comments.Select(c => {
                                   var vo = _mapper.Map<CommentListVO>(c);
                                   if (userDic.TryGetValue(c.CommentUserId, out var user)) vo.CommentUserName = user.Username;
                                   return vo;
                               })
                       .ToList();
    }

    public async Task<bool> IsChecked(CommentIsCheckDTO isCheckDto) {
        var comment = await commentRepository.SetChecked(isCheckDto.Id, isCheckDto.IsCheck);
        if (comment == null) return false;
        var articleId = comment.TypeId;
        const string redisKey = RedisConst.ARTICLE_COMMENT_COUNT;
        var field = articleId.ToString();
        var delta = isCheckDto.IsCheck == SQLConst.COMMENT_IS_CHECK ? 1 : -1;
        await redisBasketRepository.HashIncrementAsync(redisKey, field, delta);
        return true;
    }

    public async Task<(bool isSuccess, string? msg)> Delete(long commentId) {
        if ((await Db.Queryable<Comment>().Where(c => c.ParentId == commentId).ToListAsync()).Count > 0) return (false, "该评论还有子评论");
        if (!await commentRepository.DeleteByIds([commentId])) return (false, null);
        await likeRepository.Delete(l => l.Type == (int)LikeType.Comment && l.TypeId == commentId);
        return (true, null);
    }
}