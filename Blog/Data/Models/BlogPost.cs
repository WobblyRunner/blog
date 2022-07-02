

namespace Blog.Data.Models;

// Disabling nullable warnings ; all of this is data as a class ; may change at a later date TODO
#nullable disable

[Table("BLOG_POSTS")]
public class BlogPost
{
	/// <summary> Primary Key as GUID. Automatically generated value. </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid PostID { get; set; }

	/// <summary> Title of the blog post </summary>
	[Required] [StringLength(60)] public string Title { get; set; }
	/// <summary> Contents of the blog post </summary>
	[Required] [StringLength(4_000)] public string Content { get; set; }

	/// <summary> Blog post descriptive tags </summary>
	[StringLength(600)] public string Tags { get; set; }

	private const int MEG = 1_028*1_028;
	/// <summary> Thumbnail image of the blog post </summary>
	[MaxLength(16 * MEG)] public byte[] Thumbnail { get; set; }
}