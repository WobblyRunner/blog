using Blog.Data.Models;

namespace Blog.Services;

public interface IBlogService
{
	/// <summary> Create a new BlogPost in the database </summary>
	/// <param name="newBlogPost"> New blog post to create </param>
	/// <returns> Newly created BlogPost with updated properties </returns>
	public ValueTask<BlogPost?> CreatePost(BlogPost newBlogPost);

	/// <summary> Delete a BlogPost by ID from the database </summary>
	/// <returns> True on success</returns>
	public ValueTask<BlogPost?> DeletePost(Guid id);

	/// <summary> Gets BlogPost by ID from the database </summary>
	/// <returns> The BlogPost if the ID exists, otherwise null </returns>
	public ValueTask<BlogPost?> GetPostById(Guid id);

	/// <summary> Get top most-recently created posts </summary>
	/// <param name="count"> How many posts to grab from the top. Default: 5 </param>
	/// <returns> IEnumerable object of found BlogPost objects </returns>
	public ValueTask<IEnumerable<BlogPost>> GetMostRecent(int count = 5);


}