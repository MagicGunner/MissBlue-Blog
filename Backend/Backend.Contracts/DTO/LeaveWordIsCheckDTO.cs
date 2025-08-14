using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class LeaveWordIsCheckDTO {
    /// <summary>
    /// 留言id
    /// </summary>
    [Required(ErrorMessage = "留言id不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 是否通过
    /// </summary>
    [Required(ErrorMessage = "是否通过不能为空")]
    public int IsCheck { get; set; }
}