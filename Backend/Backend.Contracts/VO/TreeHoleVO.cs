namespace Backend.Contracts.VO;

/// <summary>
/// 树洞视图对象（TreeHoleVO）
/// </summary>
public class TreeHoleVO {
    /// <summary>
    /// 用户昵称
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// 用户头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }
}