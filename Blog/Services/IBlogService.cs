using Blog.Data.Models;

namespace Blog.Services;

public interface IBlogService
{
	/// <summary> Create a new BlogPost in the </summary>
	/// <param name="newBlogPost"> New blog post to create </param>
	/// <returns> True on success. </returns>
	public bool CreatePost(BlogPost newBlogPost);

	/// <summary> Delete a BlogPost by ID from the database </summary>
	/// <returns> True on success</returns>
	public bool DeletePost(Guid id);

	/// <summary> Gets BlogPost by ID from the database </summary>
	/// <returns> The BlogPost if the ID exists, otherwise null</returns>
	public BlogPost? GetPostById(Guid id);


}