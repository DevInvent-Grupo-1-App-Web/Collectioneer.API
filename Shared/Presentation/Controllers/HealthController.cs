using Collectioneer.API.Shared.Application.Internal.Services;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Shared.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HealthController(
		InstanceHealthService instanceHealthService
	) : ControllerBase
	{
		private readonly InstanceHealthService _instanceHealthService = instanceHealthService;

		[HttpGet]
		public async Task<ObjectResult> GetHealthStatus()
		{/*
			try
			{*/
				if (_instanceHealthService == null)
				{
					return StatusCode(500, "Health service is not available.");
				}

				var healthStatus = await _instanceHealthService.IsHealthy();

				if (healthStatus)
				{
					return Ok(_instanceHealthService.GetHealthReport());
				}
				else
				{
					return StatusCode(503, _instanceHealthService.GetHealthReport());
				}
			/*}
			catch
			{
				return StatusCode(500, "An error occurred while checking the health of the services. Expect degraded performance.");
			}*/
		}
	}
}