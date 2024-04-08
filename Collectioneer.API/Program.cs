
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Social.Application.Internal.MappingProfiles;
using Collectioneer.API.Social.Application.Internal.Services;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;
using Collectioneer.API.Social.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using PhoneResQ.API.Shared.Infrastructure.Repositories;
using System;

namespace Collectioneer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Database Connection


            var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");

            builder.Services.AddDbContext<AppDbContext>(
                options =>
                {
                    if (connectionString != null)
                    {
                        options.UseMySQL(connectionString)
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                    }
                }
            );

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);


            var app = builder.Build();

            // Test DB connection
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AppDbContext>();
                try
                {
                    context.Database.OpenConnection();
                    context.Database.CloseConnection();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
                    return;
                }
            }

            app.UseCors("AllowAll");

            using (var scope = app.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<AppDbContext>())
            {
                context?.Database.EnsureCreated();
            }

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
