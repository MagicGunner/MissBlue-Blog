namespace Backend.Common.Const;

public static class RedisConst {
    /// <summary>JWT 黑名单</summary>
    public const string JWT_WHITE_LIST = "jwt:white:list:";

    /// <summary>邮箱验证码</summary>
    public const string VERIFY_CODE = "verifyCode:";

    /// <summary>邮箱验证码过期时间（分钟）</summary>
    public const int VERIFY_CODE_EXPIRATION = 5;

    /// <summary>分隔符</summary>
    public const string SEPARATOR = ":";

    /// <summary>注册</summary>
    public const string REGISTER = "register";

    /// <summary>重置密码</summary>
    public const string RESET = "reset";

    /// <summary>重置邮箱</summary>
    public const string RESET_EMAIL = "resetEmail";

    /// <summary>文章收藏数</summary>
    public const string ARTICLE_FAVORITE_COUNT = "article:count:favorite:";

    /// <summary>文章点赞数</summary>
    public const string ARTICLE_LIKE_COUNT = "article:count:like:";

    /// <summary>文章评论数</summary>
    public const string ARTICLE_COMMENT_COUNT = "article:count:comment:";

    /// <summary>文章访问数</summary>
    public const string ARTICLE_VISIT_COUNT = "article:count:visit:";

    /// <summary>文章访问量限制key</summary>
    public const string ARTICLE_VISIT_COUNT_LIMIT = "article:count:visit:limit:";

    /// <summary>文章访问量新增间隔时间（单位：秒）</summary>
    public const int ARTICLE_VISIT_COUNT_INTERVAL = 15 * 60;

    /// <summary>邮箱确认友链通过状态码</summary>
    public const string EMAIL_VERIFICATION_LINK = "email:verification:link:";

    /// <summary>黑名单：用户UID</summary>
    public const string BLACK_LIST_UID_KEY = "blackList:uid:";

    /// <summary>黑名单：IP</summary>
    public const string BLACK_LIST_IP_KEY = "blackList:ip:";
}