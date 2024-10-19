using Collectioneer.API.Shared.Application.External.Services;
using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Shared.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HealthController(
		AppDbContext dbContext,
		IMediaElementService mediaElementService,
		CommunicationService communicationService,
		IContentModerationService contentModerationService
	) : ControllerBase
	{
		AppDbContext _dbContext = dbContext;
		IMediaElementService _mediaElementService = mediaElementService;
		CommunicationService _communicationService = communicationService;
		IContentModerationService _contentModerationService = contentModerationService;

		[HttpGet]
		public async Task<ObjectResult> Get()
		{
			try
			{
				bool isDatabaseConnectionOk;
				bool isStorageConnectionOk = await _mediaElementService.IsStorageConnectionOk();
				bool isEmailConnectionOk = await _communicationService.IsEmailConnectionOk();
				bool isContentModerationConnectionOk = await _contentModerationService.IsContentModerationServiceOk();
	
				try
				{
					await _dbContext.Database.CanConnectAsync();
					isDatabaseConnectionOk = true;
				}
				catch
				{
					try
					{
						await _dbContext.Database.CanConnectAsync();
						isDatabaseConnectionOk = true;
					}
					catch
					{
						isDatabaseConnectionOk = false;
					}
				}
	
				var statusCode = isDatabaseConnectionOk && isStorageConnectionOk && isEmailConnectionOk && isContentModerationConnectionOk ? 200 : 500;
	
	
				return StatusCode(statusCode, $@"
				{{
					""database"": {isDatabaseConnectionOk},
					""storage"": {isStorageConnectionOk},
					""email"": {isEmailConnectionOk},
					""contentModeration"": {isContentModerationConnectionOk}
				}}
				");
			}
			catch (Exception)
			{
				return StatusCode(500, "An error occurred while checking the health of the services.");
			}

		}
	}
}