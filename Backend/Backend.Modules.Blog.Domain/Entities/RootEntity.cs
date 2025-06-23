using SqlSugar;

namespace Backend.Modules.Blog.Domain.Entities;

public abstract class RootEntity<TKey> where TKey : IEquatable<TKey> {
    /// <summary>
    /// ID 主键ID（如果是主键自增，设定 IsPrimaryKey = true, IsIdentity = true）
    /// 泛型主键Tkey
    /// </summary>
    [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
    public TKey Id { get; set; }
}