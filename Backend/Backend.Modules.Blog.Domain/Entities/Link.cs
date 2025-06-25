using Backend.Domain.Entity;
using SqlSugar;

namespace Backend.Modules.Blog.Domain.Entities;

[SugarTable("t_link")]
public class Link : RootEntity {
    /// <summary>
    /// 用户ID（关联谁添加的友链）
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 网站名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 网站地址（URL）
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 网站描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 网站背景（如封面图地址）
    /// </summary>
    public string Background { get; set; }

    /// <summary>
    /// 联系邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 创建时间（插入时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间（插入和更新时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 审核状态（0：未通过，1：已通过）
    /// </summary>
    public int IsCheck { get; set; }

    /// <summary>
    /// 是否删除（0：未删除，1：已删除）
    /// </summary>
    public int IsDeleted { get; set; }
}