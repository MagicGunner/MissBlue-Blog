using System.ComponentModel;

namespace Backend.Common.Enums;

public enum RespEnum {
    [Description("操作成功")]
    Success = 200,

    [Description("操作失败")]
    Failure = 500,

    [Description("用户名或密码错误")]
    UsernameOrPasswordError = 1001,

    [Description("请先登录")]
    NotLogin = 1002,

    [Description("没有权限")]
    NoPermission = 1003,

    [Description("请求频繁")]
    RequestFrequently = 1004,

    [Description("验证码错误")]
    VerifyCodeError = 1005,

    [Description("用户名或邮箱已存在")]
    UsernameOrEmailExist = 1006,

    [Description("参数错误")]
    ParamError = 1007,

    [Description("其他故障")]
    OtherError = 1008,

    [Description("会话数量已达上限")]
    SessionLimit = 1009,

    [Description("请先删除子菜单")]
    NoDeleteChildMenu = 1010,

    [Description("文件上传错误")]
    FileUploadError = 1011,

    [Description("账号被封禁")]
    BlackListError = 1012
}