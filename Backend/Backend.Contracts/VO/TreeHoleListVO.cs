namespace Backend.Contracts.VO;

/// <summary>
/// 树洞列表视图对象（TreeHoleListVO）
/// </summary>
public class TreeHoleListVO {
    /// <summary>
    /// 树洞表ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 是否通过（0：否，1：是）
    /// </summary>
    public int IsCheck { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}