namespace Backend.Contracts.VO;

/// <summary>
/// 分页视图对象
/// </summary>
/// <typeparam name="T">分页数据类型</typeparam>
public class PageVO<T> {
    /// <summary>
    /// 分页数据
    /// </summary>
    public T Page { get; set; }

    /// <summary>
    /// 数据总数
    /// </summary>
    public long Total { get; set; }
}