namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 初始化搜索标题视图对象（InitSearchTitleVO）
/// </summary>
public class InitSearchTitleVO {
    /// <summary>
    /// 文章ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// 访问量
    /// </summary>
    public long VisitCount { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    public string ArticleTitle { get; set; }
}