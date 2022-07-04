using AutoMapper;
using Blog.Data.Models;
using Blog.Data.Models.DTOs;

namespace Blog.Configuration;

public class BlogProfile : Profile
{
	public BlogProfile()
	{
		CreateMap<Image, ImageUploadDTO>().ReverseMap();
	}
}