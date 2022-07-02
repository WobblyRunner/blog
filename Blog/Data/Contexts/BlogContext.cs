using System.Data.Entity;
using Blog.Data.Models;

namespace Blog.Data.Contexts;

public class BlogContext : DbContext
{
	public DbSet<BlogPost> BlogPosts { get; set; } = null!;


}