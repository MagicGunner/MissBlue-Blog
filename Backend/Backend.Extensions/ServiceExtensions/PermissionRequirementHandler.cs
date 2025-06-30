using Microsoft.AspNetCore.Authorization;

namespace Backend.Extensions.ServiceExtensions;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement> {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement) {
        var permissions = context.User.Claims
                                 .Where(c => c.Type == "Permission")
                                 .Select(c => c.Value);

        if (permissions.Contains(requirement.PermissionKey)) {
            context.Succeed(requirement);
        } else {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}