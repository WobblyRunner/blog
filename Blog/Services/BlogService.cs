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

	#region Blog Posts
	public async ValueTask<BlogPost?> CreatePost(BlogPost newBlogPost)
	{
		using var context = _contextFactory.CreateDbContext();

		/* Set the dates for the new blog post automatically */
		newBlogPost.DateCreated = DateTime.Now;
		newBlogPost.DateModified = DateTime.Now;

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
		if (!BlogPostExists(id))
			return null;

		using var context = _contextFactory.CreateDbContext();
		return await context.BlogPosts.FirstOrDefaultAsync(post => post.PostID == id);
	}

	public async ValueTask<IEnumerable<BlogPost>> GetMostRecent(int count = 5)
	{
		using var context = _contextFactory.CreateDbContext();
		return count > 0 ? await context.BlogPosts.OrderByDescending(post => post.DateCreated).Take(count).ToArrayAsync() : Array.Empty<BlogPost>();
	}

	private bool BlogPostExists(Guid id)
	{
		using var context = _contextFactory.CreateDbContext();
		return context.BlogPosts.Any(post => post.PostID == id);
	}
	#endregion
	#region Image Service
	public async ValueTask<Image?> UploadImage(Image image)
	{
		using var context = _contextFactory.CreateDbContext();

		var tracking = context.Images.Add(image);
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

	public async ValueTask<Image?> UploadImage(byte[] blob, string fileName, string extension)
	{
		Image image = new()
		{
			ImageBlob = blob,
			FileName = fileName,
			Extension = extension
		};

		/* Add generated Image model to database */
		using var context = _contextFactory.CreateDbContext();

		var tracking = context.Images.Add(image);
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
	#endregion
}