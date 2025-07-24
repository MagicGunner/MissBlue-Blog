using System.ComponentModel;

namespace Backend.Common.Extensions;

public enum BlackListEnum {
    [Description("黑名单类型：用户")]
    BlackListTypeUser = 1,

    [Description("黑名单类型：路人或攻击者")]
    BlackListTypeBot = 2
}