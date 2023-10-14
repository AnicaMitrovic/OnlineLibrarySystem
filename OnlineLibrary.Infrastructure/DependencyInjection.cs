using Microsoft.Extensions.DependencyInjection;
using OnlineLibrary.Application.Interfaces;
using OnlineLibrary.Infrastructure.Data;
using OnlineLibrary.Infrastructure.DataModels;
using OnlineLibrary.Infrastructure.Repos;
using OnlineLibrary.Application.Services;

namespace OnlineLibrary.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<AppDbContext>();

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            return services;
        }
    }
}
