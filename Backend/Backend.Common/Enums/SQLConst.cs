namespace Backend.Common.Enums;

public class SQLConst {
    /// <summary>
    /// 热门文章数量
    /// </summary>
    public static readonly int HotArticleCount = 5;

    /**
    * 推荐文章的字段标识
    */
    public static readonly int RECOMMEND_ARTICLE = 1;

    /**
     * 随机文章数量
     */
    public static readonly int RANDOM_ARTICLE_COUNT = 5;

    /**
     * 相关文章数量
     */
    public static readonly int RELATED_ARTICLE_COUNT = 5;

    /**
     * 公开文章的字段标识
     */
    public static readonly int PUBLIC_ARTICLE = 1;

    /**
     * 公开
     */
    public static readonly int STATUS_PUBLIC = 1;

    /**
     * 评论是否通过(0,否)
     */
    public static readonly int COMMENT_IS_CHECK = 1;

    /**
     * 是否通过(0,否)
     */
    public static readonly string IS_CHECK = "is_check";

    /**
     * 通过
     */
    public static readonly int IS_CHECK_YES = 1;

    /**
     * 创建时间
     */
    public static readonly string CREATE_TIME = "create_time";

    /**
     * id
     */
    public static readonly string ID = "id";

    /**
     * 管理员id
     */
    public static readonly long ADMIN_ID = 1L;

    /**
     * 查询一条
      */
    public static readonly string LIMIT_ONE_SQL = "LIMIT 1";

    /**
     * Banner最多数量
     */
    public static readonly int BANNER_MAX_COUNT = 5;

    /**
     * 优先显示相册，再显示照片，时间倒序
     */
    public static readonly string ORDER_BY_CREATE_TIME_DESC = "ORDER BY CASE WHEN type = 1 THEN 0 ELSE 1 END, create_time DESC";
}