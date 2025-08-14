using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class DeletePhotoOrAlbumDTO {
    /// <summary>
    /// 删除照片或相册 DTO
    /// </summary>
    public class DeletePhotoOrAlbumDto {
        /// <summary>
        /// 主键 ID（照片或相册 ID）
        /// </summary>
        [Required(ErrorMessage = "id不能为空")]
        public long Id { get; set; }

        /// <summary>
        /// 类型（1: 相册，2: 照片）
        /// </summary>
        [Range(1, 2, ErrorMessage = "参数错误")]
        public int Type { get; set; }

        /// <summary>
        /// 所属父相册 ID（仅删除照片时需要）
        /// </summary>
        public long? ParentId { get; set; }
    }
}