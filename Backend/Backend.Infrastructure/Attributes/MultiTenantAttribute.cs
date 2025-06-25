using Backend.Domain.Entity.Tenants;

namespace Backend.Infrastructure.Attributes;

public class MultiTenantAttribute : Attribute {
    public MultiTenantAttribute() {
    }

    public MultiTenantAttribute(TenantTypeEnum tenantType) {
        TenantType = tenantType;
    }


    public TenantTypeEnum TenantType { get; set; }
}