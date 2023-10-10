using Microsoft.Extensions.DependencyInjection;
using OnlineLibrary.Application.Middlewares;

namespace OnlineLibrary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddTransient<GlobalExceptionHandlingMiddleware>();
            services.AddTransient<CustomHeaderMiddlerware>();

            return services;
        }
    }
}
