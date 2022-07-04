using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Blog.Configuration;
using Blog.Data.Contexts;
using Blog.Data.Models;
using Microsoft.EntityFrameworkCore;
using Blog.Services.BlobStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add AutoMapper for mapping of DTOs to models
builder.Services.AddAutoMapper(typeof(BlogProfile));

// Add Entity Framework DbContext instances
var blogConnectionString = builder.Configuration.GetConnectionString("conn_blog");
builder.Services.AddDbContextFactory<BlogContext>(options => options.UseSqlServer(blogConnectionString));

// Add Azure Blob Storage
builder.Services.AddTransient<BlobService>();

// Add our Service-Based infrastructure
builder.Services.AddSingleton<IBlogService, BlogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
