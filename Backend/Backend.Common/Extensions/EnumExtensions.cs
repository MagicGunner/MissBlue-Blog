using System.ComponentModel;
using System.Reflection;

namespace Backend.Common.Extensions;

public static class EnumExtensions {
    public static string GetDescription(this Enum value) {
        var type = value.GetType();
        var member = type.GetMember(value.ToString());

        if (member.Length <= 0) return value.ToString();
        var attr = member[0].GetCustomAttribute<DescriptionAttribute>();
        return attr != null ? attr.Description : value.ToString();
    }
}