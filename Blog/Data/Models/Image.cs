namespace Blog.Data.Models;

[Table("IMAGES")]
public class Image
{
	/// <summary> Primary key as GUID. Automatically generated value. </summary>
	[Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid ImageID { get; set; }

	/// <summary> Name of corresponding Blob in server </summary>
	[Required] public string BlobURI { get; set; } = null!;

	/// <summary> Filename of uploaded image without Guid prefix </summary>
	[Required] [StringLength(100)] public string FileName { get; set; } = null!;

	/// <summary> Extension of the image, used to determine the image type (JPG, PNG, WEBM, etc.) </summary>
	[Required] [StringLength(10)] public string Extension { get; set; } = null!;

	/// <summary> Descriptive title of the image, for accessibility </summary>
	[StringLength(60)] public string? Title { get; set; } = null!;

	/// <summary> Description caption of the image, for accessibility </summary>
	[StringLength(200)] public string? Caption { get; set; } = null!;
}