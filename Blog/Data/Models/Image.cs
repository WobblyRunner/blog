namespace Blog.Data.Models;

[Table("IMAGES")]
public class Image
{
	/// <summary> Primary key as GUID. Automatically generated value. </summary>
	[Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid ImageID { get; set; }

	/// <summary> Blob of image as Byte Array </summary>
	[Required] public byte[] ImageBlob { get; set; } = null!;

	/// <summary> File name of uploaded image without extension </summary>
	[Required] [StringLength(60)] public string FileName { get; set; } = null!;

	/// <summary> Descriptive title of the image, for accessibility </summary>
	[StringLength(60)] public string? Title { get; set; } = null!;

	/// <summary> Description caption of the image, for accessibility </summary>
	[StringLength(200)] public string? Caption { get; set; } = null!;

	/// <summary> Extension of the image, used to determine the image type (JPG, PNG, WEBM, etc.) </summary>
	[Required] [StringLength(10)] public string Extension { get; set; } = null!;
}