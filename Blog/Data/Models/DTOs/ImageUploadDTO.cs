namespace Blog.Data.Models.DTOs;

/// <summary> Used for uploading images to the database </summary>
public class ImageUploadDTO
{
	[Required] [StringLength(100)] public string FileName { get; set; } = null!;
	[Required] [StringLength(10)] public string Extension { get; set; } = null!;
	[StringLength(60)] public string? Title { get; set; }
	[StringLength(200)] public string? Caption { get; set; }
}