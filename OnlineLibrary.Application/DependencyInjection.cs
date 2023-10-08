using Microsoft.Extensions.DependencyInjection;

namespace OnlineLibrary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
