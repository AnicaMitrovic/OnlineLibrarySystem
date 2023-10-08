using OnlineLibrary.Application;
using OnlineLibrary.Application.Interfaces;
using OnlineLibrary.Infrastructure;
using OnlineLibrary.Infrastructure.Data;
using OnlineLibrary.Infrastructure.DataModels;
using OnlineLibrary.Infrastructure.Interfaces;
using OnlineLibrary.Infrastructure.Services;

namespace OnlineLibrarySystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuring separate layers with dependency injection
            builder.Services
                .AddApplication()
                .AddInfrastructure();

            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            builder.Services.AddSingleton<AppDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}