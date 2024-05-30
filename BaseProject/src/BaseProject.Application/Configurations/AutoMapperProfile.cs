using AutoMapper;
using BaseProject.Application.Models.Responses;
using BaseProject.Domain.Entities;

namespace BaseProject.Application.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.LovedBooks, opt => opt.MapFrom(src => src.LovedBooks));

            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

            CreateMap<LovedBook, LovedBookResponse>()
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.Name));
        }
    }
}