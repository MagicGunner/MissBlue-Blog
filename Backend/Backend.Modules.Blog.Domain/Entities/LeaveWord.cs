﻿using Backend.Domain.Entity;
using SqlSugar;

namespace Backend.Modules.Blog.Domain.Entities;

[SugarTable("t_leave_word")]
public class LeaveWord : RootEntity {
    /// <summary>
    /// 留言用户 ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 留言内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 是否通过审核（0 否，1 是）
    /// </summary>
    public int IsCheck { get; set; }

    /// <summary>
    /// 创建时间（自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间（自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0 未删除，1 已删除）
    /// </summary>
    public int IsDeleted { get; set; }
}