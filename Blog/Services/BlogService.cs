using Blog.Data.Contexts;
using Blog.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services;

public class BlogService : IBlogService
{
	private readonly IDbContextFactory<BlogContext> _contextFactory;

	public BlogService(IDbContextFactory<BlogContext> context)
	{
		_contextFactory = context;
	}

	public async ValueTask<BlogPost?> CreatePost(BlogPost newBlogPost)
	{
		using var context = _contextFactory.CreateDbContext();

		var tracking = context.BlogPosts.Add(newBlogPost);
		try
		{
			await context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			return null;
		}
		return tracking.Entity;
	}

	public async ValueTask<BlogPost?> DeletePost(Guid id)
	{
		throw new NotImplementedException();
		return null;
	}

	public async ValueTask<BlogPost?> GetPostById(Guid id)
	{
		using var context = _contextFactory.CreateDbContext();
		return await context.BlogPosts.FirstOrDefaultAsync(post => post.PostID == id);
	}

	public async ValueTask<IEnumerable<BlogPost>> GetMostRecent(int count = 5)
	{
		using var context = _contextFactory.CreateDbContext();
		return count > 0 ? await context.BlogPosts.OrderByDescending(post => post.DateCreated).Take(count).ToArrayAsync() : Array.Empty<BlogPost>();
	}

	private bool Exists(Guid id)
	{
		using var context = _contextFactory.CreateDbContext();
		return context.BlogPosts.Any(post => post.PostID == id);
	}
}