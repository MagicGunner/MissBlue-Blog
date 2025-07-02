using System.Security.Claims;
using Backend.Application.Interface;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Implement;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser {
    public long? UserId {
        get {
            var userIdClaim = httpContextAccessor.HttpContext?.User?
               .Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? long.Parse(userIdClaim.Value) : null;
        }
    }

    public string UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "";

    public List<string> Roles =>
        httpContextAccessor.HttpContext?.User?
                           .Claims.Where(c => c.Type == ClaimTypes.Role)
                           .Select(c => c.Value)
                           .ToList() ?? [];

    public List<string> Permissions =>
        httpContextAccessor.HttpContext?.User?
                           .Claims.Where(c => c.Type == "Permission")
                           .Select(c => c.Value)
                           .ToList() ?? [];

    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}