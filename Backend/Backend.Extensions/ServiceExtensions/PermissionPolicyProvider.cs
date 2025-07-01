using Backend.Domain.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Backend.Extensions.ServiceExtensions;

public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options,
                                      IPermissionRepository          permissionRepository)
    : DefaultAuthorizationPolicyProvider(options) {
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName) {
        // 先查是否已经存在Policy（静态注册过的）
        var policy = await base.GetPolicyAsync(policyName);
        if (policy != null) {
            return policy;
        }

        // 查询数据库权限
        var permissions = await permissionRepository.GetAllPermissions();
        var exists = permissions.Any(p => p.PermissionKey == policyName);

        if (!exists) {
            // 如果权限数据库中没有此PermissionKey，返回null → 无此权限
            return null;
        }

        // 动态构建权限
        return new AuthorizationPolicyBuilder()
              .AddRequirements(new PermissionRequirement(policyName))
              .Build();
    }
}