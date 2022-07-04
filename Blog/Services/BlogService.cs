using AutoMapper;
using Blog.Data.Contexts;
using Blog.Data.Models;
using Blog.Data.Models.DTOs;
using Blog.Services.BlobStorage;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services;

public class BlogService : IBlogService
{
	private readonly IDbContextFactory<BlogContext> _contextFactory;
	private readonly BlobService _blobService;
	private readonly IMapper _mapper;

	public BlogService(IDbContextFactory<BlogContext> context, BlobService blobService, IMapper mapper)
	{
		_contextFactory = context;
		_blobService = blobService;
		_mapper = mapper;
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
		using var context = _contextFactory.CreateDbContext();

		return await context.BlogPosts.FirstOrDefaultAsync(post => post.PostID == id);
	}

	public async ValueTask<IEnumerable<BlogPost>> GetMostRecent(int count = 5)
	{
		using var context = _contextFactory.CreateDbContext();
		return count > 0 ? await context.BlogPosts.OrderByDescending(post => post.DateCreated).Take(count).ToArrayAsync() : Array.Empty<BlogPost>();
	}

	private bool BlogPostExists(Guid id, BlogContext context)
	{
		return context.BlogPosts.Any(post => post.PostID == id);
	}
	#endregion
	#region Images
	public async ValueTask<Image?> UploadImage(ImageUploadDTO imageUpload, Stream imageStream)
	{
		var context = _contextFactory.CreateDbContext();

		var image = _mapper.Map<Image>(imageUpload);
		
		// First, we want to upload the blob to blob storage
		string? blobUri = await _blobService.Upload(imageStream, image.FileName);

		// Secondly, create the record in the database that expresses the link
		if (blobUri != null)
		{
			image.BlobURI = blobUri;
			bool success;
			var tracking = context.Images.Add(image);
			try
			{
				await context.SaveChangesAsync();
				success = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				success = true;
			}
			if (success)
				return tracking.Entity;
		}
		return null;
	}

	public async ValueTask<Image?> DeleteImage(Guid id)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Image?> GetImageById(Guid id)
	{
		throw new NotImplementedException();
	}

	private bool ImageExists(Guid id, BlogContext context)
	{
		return context.Images.Any(image => image.ImageID == id);
	}
	#endregion
}