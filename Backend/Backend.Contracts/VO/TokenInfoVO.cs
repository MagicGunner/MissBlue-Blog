namespace Backend.Contracts.VO;

public class TokenInfoVO {
    public bool   Success   { get; set; }
    public string Token     { get; set; }
    public int    ExpiresIn { get; set; }
    public string TokenType { get; set; } = "Bearer";
}