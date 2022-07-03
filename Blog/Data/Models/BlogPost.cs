namespace Blog.Data.Models;

[Table("BLOG_POSTS")]
public class BlogPost
{
	/// <summary> Primary Key as GUID. Automatically generated value. </summary>
	[Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid PostID { get; set; }

	/// <summary> Title of the blog post </summary>
	[Required] [StringLength(60)] public string Title { get; set; } = null!;

	/// <summary> Author of the blog post's full name </summary>
	[Required] [StringLength(60)] public string Author { get; set; } = null!;

	/// <summary> Contents of the blog post </summary>
	[Required] [StringLength(4_000)] public string Content { get; set; } = null!;

	/// <summary> Blog post descriptive tags </summary>
	[StringLength(600)] public string Tags { get; set; } = null!;

	/// <summary> Foreign key of Thumbnail Image </summary>
	[ForeignKey(nameof(Image))] public Guid? Thumbnail_ImageID { get; set; }
	/// <summary> Actual thumbnail image as Image class </summary>
	public virtual Image? Thumbnail_Image { get; set; }

	/// <summary> Date the post was created, defaults to current date and time </summary>
	[Required] public DateTime DateCreated { get; set; }

	/// <summary> Date the most was last modified by an UPDATE command </summary>
	[Timestamp] [Required] public DateTime DateModified { get; set; }
}