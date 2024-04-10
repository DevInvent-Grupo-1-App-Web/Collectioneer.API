
using Collectioneer.API.Operational.Application.Services.Internal;
using Collectioneer.API.Operational.Application.Services.Internal.MappingProfiles;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Domain.Services;
using Collectioneer.API.Operational.Infrastructure.Repositories;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Social.Application.Internal.MappingProfiles;
using Collectioneer.API.Social.Application.Internal.Services;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;
using Collectioneer.API.Social.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace Collectioneer.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			if (builder.Environment.IsDevelopment())
			{
				builder.Configuration.AddUserSecrets<Program>();
			}

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
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "API",
					Version = "v2",
					Description = "Your Api Description"
				});
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: 'Bearer 12345abcdef'",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});
				options.OperationFilter<AuthorizeCheckOperationFilter>();
			});

			// Add Database Connection


			var connectionString = builder.Configuration["MYSQL_CONNECTION_STRING"];
			Console.WriteLine($"Connection String: {connectionString}");

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

            builder.Services.AddScoped<ICollectibleRepository, CollectibleRepository>();
            builder.Services.AddScoped<ICollectibleService, CollectibleService>();

            builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
            builder.Services.AddScoped<IArticleService, ArticleService>();

            builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
            builder.Services.AddScoped<IAuctionService, AuctionService>();

            builder.Services.AddScoped<IBidRepository, BidRepository>();


            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(CollectibleProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(ArticleProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(AuctionProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(BidProfile).Assembly);

			builder.Services.AddAuthentication(
					JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidateAudience = true,
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							ValidIssuer = builder.Configuration["JWT_ISSUER"],
							ValidAudience = builder.Configuration["JWT_AUDIENCE"],
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_KEY"]))
						};
					});


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

				app.UseAuthentication();
				app.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
				});
			}


			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}

	public class AuthorizeCheckOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			// Check for Authorize attribute
			var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
												 || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

			if (hasAuthorize)
			{
				operation.Security = new List<OpenApiSecurityRequirement>
						{
								new OpenApiSecurityRequirement
								{
										[
												new OpenApiSecurityScheme {Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}}
										] = new string[] { }
								}
						};
			}
		}
	}
}
