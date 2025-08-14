using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class PhotoAlbumDTO {
    public long? Id { get; set; }

    public long? ParentId { get; set; }

    [Required(ErrorMessage = "相册名称不能为空")]
    [MaxLength(20, ErrorMessage = "相册名称不能超过20个字符")]
    public string Name { get; set; }

    public string Description { get; set; }
}