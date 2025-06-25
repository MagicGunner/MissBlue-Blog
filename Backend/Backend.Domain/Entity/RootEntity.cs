using SqlSugar;

namespace Backend.Domain.Entity;

public abstract class RootEntity {
    /// <summary>
    /// ID 主键ID（如果是主键自增，设定 IsPrimaryKey = true, IsIdentity = true）
    /// 泛型主键Tkey
    /// </summary>
    [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
    public long Id { get; set; }
}