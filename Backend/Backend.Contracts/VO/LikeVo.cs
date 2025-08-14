namespace Backend.Contracts.VO;

public class LikeVo {
    public long UserId { get; set; }

    public int Type { get; set; }

    public long TypeId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }
}