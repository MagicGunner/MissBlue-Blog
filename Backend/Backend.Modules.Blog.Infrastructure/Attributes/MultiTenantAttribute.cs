using Backend.Modules.Blog.Domain.Entities.Tenants;

namespace Backend.Modules.Blog.Infrastructure.Attributes;

public class MultiTenantAttribute : Attribute {
    public MultiTenantAttribute() {
    }

    public MultiTenantAttribute(TenantTypeEnum tenantType) {
        TenantType = tenantType;
    }


    public TenantTypeEnum TenantType { get; set; }
}