using Backend.Common;
using Backend.Domain.Entity;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.Enums;
using Backend.Modules.Blog.Domain.IRepository;
using SqlSugar;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class CommentRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Comment>(unitOfWorkManage), ICommentRepository {
    public async Task<bool> AddComment(Comment comment) => await Add(comment) > 0;

    public async Task<Dictionary<long, long>> GetCountDic(CommentType type, List<long> typeIds) {
        return (await Db.Queryable<Comment>()
                        .Where(comment => comment.Type == (int)type && comment.IsCheck == 1 && typeIds.Contains(comment.TypeId))
                        .GroupBy(comment => comment.TypeId)
                        .Select(comment => new {
                                                   comment.TypeId,
                                                   Count = (long)SqlFunc.AggregateCount(comment.Id)
                                               })
                        .ToListAsync()).ToDictionary(i => i.TypeId, i => i.Count);
    }

    public async Task<long> GetCount(CommentType type, long typeId) => await Db.Queryable<Comment>().Where(c => c.Type == (int)type && c.IsCheck == 1 && c.TypeId == typeId).CountAsync();

    public async Task<List<Comment>> GetBackList(string? userName, string? content, int? type, int? isCheck) {
        var query = Db.Queryable<Comment>();
        // 1. 根据评论人用户名模糊搜索获取用户 ID 列表
        if (!string.IsNullOrWhiteSpace(userName)) {
            var userIds = await Db.Queryable<User>()
                                  .Where(u => u.Username.Contains(userName))
                                  .Select(u => u.Id)
                                  .ToListAsync();

            query = userIds.Count > 0 ? query.Where(c => userIds.Contains(c.CommentUserId)) : query.Where(c => c.CommentUserId == 0); // 不存在的 ID
        }

        if (!string.IsNullOrWhiteSpace(content)) query = query.Where(c => c.CommentContent.Contains(content));
        if (type != null) query = query.Where(c => c.Type == type.Value);
        if (isCheck != null) query = query.Where(c => c.IsCheck == isCheck.Value);
        return await query.OrderByDescending(c => c.CreateTime).ToListAsync();
    }
}