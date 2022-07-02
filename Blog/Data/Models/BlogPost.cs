namespace Blog.Data.Models;

// Disabling nullable warnings ; all of this is data as a class, should not require default values.
#nullable disable

public class BlogPost
{
	/// <summary> Primary Key as GUID </summary>
	public Guid PostID { get; set; }

	/// <summary> Title of the blog post </summary>
	[Required] [StringLength(60)] public string Title { get; set; }
	/// <summary> Contents of the blog post </summary>
	[Required] [StringLength(4_000)] public string Content { get; set; }

	/// <summary> Blog post descriptive tags </summary>
	[StringLength(600)] public string Tags { get; set; }

	private const int TWENTYFOUR_MEG = 1028*1028*24;
	/// <summary> Thumbnail image of the blog post </summary>
	[MaxLength(TWENTYFOUR_MEG)]public byte[] Image { get; set; }
}