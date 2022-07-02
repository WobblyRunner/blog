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

	public bool CreatePost(BlogPost newBlogPost)
	{
		using var context = _contextFactory.CreateDbContext();

		context.BlogPosts.Add(newBlogPost);
		try
		{
			context.SaveChanges();
		}
		catch (DbUpdateConcurrencyException)
		{
			return false;
		}
		return true;
	}

	public bool CreatePost(BlogPost newBlogPost, out Guid guid)
	{
		using var context = _contextFactory.CreateDbContext();

		var tracking = context.BlogPosts.Add(newBlogPost);
		try
		{
			context.SaveChanges();
		}
		catch (DbUpdateConcurrencyException)
		{
			guid = Guid.Empty;
			return false;
		}
		guid = tracking.Entity.PostID;
		return true;
	}

	public bool DeletePost(Guid id)
	{
		throw new NotImplementedException();
		return false;
	}

	public BlogPost? GetPostById(Guid id)
	{
		throw new NotImplementedException();
		return null;
	}

	private bool Exists(Guid id)
	{
		using var context = _contextFactory.CreateDbContext();
		return context.BlogPosts.Any(post => post.PostID == id);
	}
}