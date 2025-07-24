using System.ComponentModel;

namespace Backend.Common.Enums;

public enum RespDesc {
    [Description("登录成功")]
    SuccessLoginMsg,

    [Description("请先获取验证码")]
    VerifyCodeNullMsg,

    [Description("用户名或密码错误")]
    UsernameOrPasswordErrorMsg,

    [Description("该账号已被停用，无法登录")]
    AccountDisabledMsg,

    [Description("该账号为后台测试账号，无法登录前台")]
    TestAccountMsg,

    [Description("该账号不具备任何权限，无法登录")]
    NoPermissionMsg,

    [Description("首页banner数量已达上限")]
    BannerMaxCountMsg
}