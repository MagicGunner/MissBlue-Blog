using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

public class FavoriteIsCheckDTO {
    /// <summary>
    /// 收藏审核 DTO
    /// </summary>
    public class FavoriteIsCheckDto {
        /// <summary>
        /// 收藏 ID
        /// </summary>
        [Required(ErrorMessage = "收藏id不能为空")]
        public long Id { get; set; }

        /// <summary>
        /// 是否有效（0 否，1 是）
        /// </summary>
        [Required(ErrorMessage = "是否有效不能为空")]
        public int IsCheck { get; set; }
    }
}