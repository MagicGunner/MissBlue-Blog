using Backend.Common.Enums;
using Backend.Common.Record;

namespace Backend.Common.Static;

public static class UploadRules {
    private static readonly Dictionary<UploadEnum, UploadRule> Map = new() {
                                                                               [UploadEnum.WebsiteInfoAvatar] = new UploadRule("websiteInfo/avatar/", "站长头像", [
                                                                                                                                   ".jpg",
                                                                                                                                   ".jpeg",
                                                                                                                                   ".png",
                                                                                                                                   ".webp"
                                                                                                                               ], 0.3),
                                                                               [UploadEnum.WebsiteInfoBackground] = new UploadRule("websiteInfo/background/", "站长背景", [
                                                                                                                                       ".jpg",
                                                                                                                                       ".jpeg",
                                                                                                                                       ".png",
                                                                                                                                       ".webp"
                                                                                                                                   ], 0.3),
                                                                               [UploadEnum.ArticleCover] = new UploadRule("article/articleCover/", "文章封面", [
                                                                                                                              ".jpg",
                                                                                                                              ".jpeg",
                                                                                                                              ".png",
                                                                                                                              ".webp"
                                                                                                                          ], 0.3),
                                                                               [UploadEnum.ArticleImage] = new UploadRule("article/articleImage/", "文章图片", [
                                                                                                                              ".jpg",
                                                                                                                              ".jpeg",
                                                                                                                              ".png",
                                                                                                                              ".gif",
                                                                                                                              ".webp"
                                                                                                                          ], 3.0),
                                                                               [UploadEnum.UserAvatar] = new UploadRule("user/avatar/", "用户头像", [
                                                                                                                            ".jpg",
                                                                                                                            ".jpeg",
                                                                                                                            ".png",
                                                                                                                            ".webp"
                                                                                                                        ], 0.3),
                                                                               [UploadEnum.UiBanners] = new UploadRule("banners/", "前台首页Banners图片", [
                                                                                                                           ".jpg",
                                                                                                                           ".jpeg",
                                                                                                                           ".png",
                                                                                                                           ".webp"
                                                                                                                       ], 0.3),
                                                                               [UploadEnum.PhotoAlbum] = new UploadRule("photoAlbum/", "相册模块图片", [
                                                                                                                            ".jpg",
                                                                                                                            ".jpeg",
                                                                                                                            ".png",
                                                                                                                            ".webp",
                                                                                                                            ".gif"
                                                                                                                        ], 4.0),
                                                                               [UploadEnum.Other] = new UploadRule("other/", "其他文件", [
                                                                                                                       ".zip",
                                                                                                                       ".docx",
                                                                                                                       ".pdf"
                                                                                                                   ], 10.0)
                                                                           };

    public static UploadRule Get(UploadEnum type) => Map[type];
}