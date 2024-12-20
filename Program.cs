using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Operational.Infrastructure.Repositories;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Shared.Application.Internal.Services;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Infrastructure.Repositories;
using Collectioneer.API.Social.Domain.Services;
using Collectioneer.API.Social.Application.Internal.Services;
using Collectioneer.API.Operational.Application.Internal.Services;
using Collectioneer.API.Shared.Application.External.Services;

namespace Collectioneer.API
{
	public class Program
	{
		private static ILogger<Program>? _logger;

		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			ConfigureEnvironment(builder);
			ConfigureJwt(builder);
			ConfigureServices(builder);

			var app = builder.Build();

			app.Use(async (context, next) =>
			{
				if (context.Request.Path == "/")
				{
					context.Response.Redirect("https://collectioneer.tqrou.me");
				}
				else
				{
					await next.Invoke();
				}
			});

			_logger = app.Services.GetRequiredService<ILogger<Program>>();

			_logger.LogInformation("Starting up Collectioneer.API");

			var appKeys = app.Services.GetRequiredService<AppKeys>();
			appKeys.CheckKeys();

			_logger.LogInformation("Checking database connection...");

			TestDbConnection(app);
			InitializeDatabase(app);

			ConfigureHttpPipeline(app);

			_logger.LogInformation("Running app...");
			_logger.LogInformation($"App is listening at port {app.Configuration["ASPNETCORE_URLS"]}");

			app.Run();

		}

		private static void ConfigureHttpPipeline(WebApplication app)
		{
			app.UseCors("AllowAll");

			app.UseSwagger();

			app.UseAuthentication();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
			});

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();
		}

		private static void InitializeDatabase(WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			using var context = scope.ServiceProvider.GetService<AppDbContext>();
			var resetDatabase = app.Configuration["RESET_DATABASE"];
			if (resetDatabase == "true")
			{
				context?.Database.EnsureDeleted();
			}
			context?.Database.EnsureCreated();
			context?.RunSqlScript("./Scripts/Startup.sql");
		}

		private static void TestDbConnection(WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			var context = scope.ServiceProvider.GetService<AppDbContext>();
			try
			{
				context?.Database.OpenConnection();
				_logger?.LogInformation("Database connection successful.");
				context?.Database.CloseConnection();
			}
			catch (Exception ex)
			{
				_logger?.LogError($"Error while connecting to database: {ex.Message} - {ex.StackTrace}");
				throw new Exception("Unable to connect to database. Cancelling startup.", ex);
			}
		}


		private static void ConfigureEnvironment(WebApplicationBuilder builder)
		{
			if (builder.Environment.IsDevelopment())
			{
				builder.Configuration.AddUserSecrets<Program>();
			}

			builder.Configuration.AddEnvironmentVariables();
		}

		private static void ConfigureJwt(WebApplicationBuilder builder)
		{
			var (issuer, audience, key) = ValidateJwtConfiguration(builder.Configuration);

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
						ValidIssuer = issuer,
						ValidAudience = audience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
					};
				});
		}

		public static (string, string, string) ValidateJwtConfiguration(IConfiguration configuration)
		{
			try
			{
				string issuer = configuration["JWT_ISSUER"] ?? throw new NullReferenceException("JWT_ISSUER");
				string audience = configuration["JWT_AUDIENCE"] ?? throw new NullReferenceException("JWT_AUDIENCE");
				string key = configuration["JWT_KEY"] ?? throw new NullReferenceException("JWT_KEY");

				return (issuer, audience, key);
			}
			catch (NullReferenceException ex)
			{
				throw new ArgumentException($"Environment variable not found: {ex.Message}. Please check your configuration. Startup failed. No server started.");
			}
		}

		private static void ConfigureServices(WebApplicationBuilder builder)
		{
			// Add services to the container.
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", corsPolicyBuilder =>
				{
					corsPolicyBuilder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
				});
			});

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			ConfigureSwagger(builder);
			ConfigureDatabase(builder);
			RegisterServices(builder);
		}

		private static void ConfigureSwagger(WebApplicationBuilder builder)
		{
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Collectioneer.API",
					Version = "v1",
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
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
				options.OperationFilter<AuthorizeCheckOperationFilter>();
			});
		}


		private static void ConfigureDatabase(WebApplicationBuilder builder)
		{
			var connectionString = builder.Configuration["MYSQL_CONNECTION_STRING"];

			builder.Services.AddDbContext<AppDbContext>(
				options =>
				{
					if (connectionString != null)
					{
						options.UseMySQL(connectionString);
					}
				}
			);

			builder.Services.AddRouting(options => options.LowercaseUrls = true);
		}

		private static void RegisterServices(WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IUserService, UserService>();

			builder.Services.AddScoped<ICollectibleRepository, CollectibleRepository>();
			builder.Services.AddScoped<ICollectibleService, CollectibleService>();

			builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
			builder.Services.AddScoped<IAuctionService, AuctionService>();

			builder.Services.AddScoped<IBidRepository, BidRepository>();

			builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();
			builder.Services.AddScoped<ICommunityService, CommunityService>();

			builder.Services.AddScoped<IRoleRepository, RoleRepository>();
			builder.Services.AddScoped<IRoleService, RoleService>();

			builder.Services.AddScoped<IMediaElementService, MediaElementService>();
			builder.Services.AddScoped<IMediaElementRepository, MediaElementRepository>();

			builder.Services.AddScoped<ICommentRepository, CommentRepository>();
			builder.Services.AddScoped<ICommentService, CommentService>();

			builder.Services.AddScoped<IPostRepository, PostRepository>();
			builder.Services.AddScoped<IPostService, PostService>();

			builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
			builder.Services.AddScoped<IReviewService, ReviewService>();

			builder.Services.AddScoped<IContentModerationService, ContentModerationService>();
			builder.Services.AddScoped<CommunicationService>();

			builder.Services.AddHttpContextAccessor();
			
			builder.Services.AddSingleton<AppKeys>();

			builder.Services.AddSingleton<Func<IServiceScope>>(sp => () => sp.CreateScope());
			builder.Services.AddSingleton<InstanceHealthService>(sp =>
			{
				var appKeys = sp.GetRequiredService<AppKeys>();
				var scopeFactory = sp.GetRequiredService<Func<IServiceScope>>();
				return new InstanceHealthService(
					appKeys.HealthStatusParams.EmailHealthCheckInterval,
					appKeys.HealthStatusParams.StorageAccountHealthCheckInterval,
					appKeys.HealthStatusParams.DatabaseHealthCheckInterval,
					appKeys.HealthStatusParams.ContentModerationHealthCheckInterval,
					scopeFactory
				);
			});


			builder.Services.AddScoped<ContentModerationService>();
			builder.Services.AddScoped<CommunicationService>();
		}


	}

	public class AuthorizeCheckOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var hasAuthorize = context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

			if (hasAuthorize)
			{
				operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
				operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

				operation.Security = new List<OpenApiSecurityRequirement>
				{
					new() {
						[
							new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								},
								Scheme = "oauth2",
								Name = "Bearer",
								In = ParameterLocation.Header,

							}
						] = new List<string>()
					}
				};
			}
		}
	}
}