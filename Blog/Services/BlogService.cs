using Blog.Data.Contexts;
using Blog.Data.Models;
using Blog.Services.BlobStorage;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services;

public class BlogService : IBlogService
{
	private readonly IDbContextFactory<BlogContext> _contextFactory;
	private readonly BlobService _fileUploadService;

	public BlogService(IDbContextFactory<BlogContext> context, BlobService fileUpload)
	{
		_contextFactory = context;
		_fileUploadService = fileUpload;
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
	#region Images
	// Just realized this function is a bad idea.
	// Needs to be changed to CreateImageAsKey(Stream stream)
	//	and all values should be random, but certain to fit table constraints
	// For now... This will work
	public async ValueTask<Image?> CreateImage(string fileName, string extension, Stream stream, string? title = null, string? caption = null)
	{
		var context = _contextFactory.CreateDbContext();

		Image image = new()
		{
			FileName = fileName,
			Extension = extension,
			Title = title,
			Caption = caption
		};

		// Create the record in the SQL Database for tracking
		var tracking = context.Images.Add(image);
		try
		{
			await context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			return null;
		}

		// Upload the image blob to the server
		var createdGuid = tracking.Entity.ImageID;
		if (createdGuid != Guid.Empty)
		{
			string uniqueFileName = $"{{{createdGuid}}}_{fileName}";
			var success = await _fileUploadService.Upload(uniqueFileName, stream);
			if (success)
				Console.WriteLine($"Successfully created file {uniqueFileName}, bound to ImageID {createdGuid}");
		}

		return tracking.Entity;
	}

	public async ValueTask<Image?> DeleteImage(Guid id)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Image?> GetImageById(Guid id)
	{
		throw new NotImplementedException();
	}
	#endregion
}