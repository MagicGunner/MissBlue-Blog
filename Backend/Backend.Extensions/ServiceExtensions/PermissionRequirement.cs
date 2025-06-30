using Microsoft.AspNetCore.Authorization;

namespace Backend.Extensions.ServiceExtensions;

public class PermissionRequirement(string permissionKey) : IAuthorizationRequirement {
    public string PermissionKey { get; } = permissionKey;
}