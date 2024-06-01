using AutoMapper;
using BaseProject.Application.Configurations;
using BaseProject.Application.Services;
using BaseProject.Application.Services.Impl;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.Helpers;
using BaseProject.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace BaseProject.Application
{
    public static class Extensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<ICryptographyHelper, CryptographyHelper>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILovedBookService, LovedBookService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBorrowingService, BorrowingService>();
            services.AddScoped<IBorrowingDetailService, BorrowingDetailService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IRatingService, RatingService>();
        }
    }
}