using System.Reflection;

namespace Backend.Common.Extensions;

public static class MethodInfoExtensions {
    public static string GetFullName(this MethodInfo method) {
        return method.DeclaringType == null ? $@"{method.Name}" : $"{method.DeclaringType.FullName}.{method.Name}";
    }
}