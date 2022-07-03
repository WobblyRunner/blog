using Microsoft.EntityFrameworkCore;
using Blog.Data.Models;

namespace Blog.Data.Contexts;

public class BlogContext : DbContext
{
	public BlogContext(DbContextOptions<BlogContext> options) : base(options)
	{
	}

	public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
	public DbSet<Image> Images => Set<Image>();
}