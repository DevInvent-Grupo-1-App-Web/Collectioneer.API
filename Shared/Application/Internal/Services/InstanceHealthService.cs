using Collectioneer.API.Shared.Application.External.Services;
using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Shared.Infrastructure.Configuration;

namespace Collectioneer.API.Shared.Application.Internal.Services
{
	public class InstanceHealthService(
		int emailHealthCheckInterval,
		int storageAccountHealthCheckInterval,
		int databaseHealthCheckInterval,
		int contentModerationHealthCheckInterval,
		Func<IServiceScope> scopeFactory
		)
	{
		public bool LastEmailHealthCheckResult { get; private set; }
		public readonly int EmailHealthCheckInterval = emailHealthCheckInterval;
		public DateTime LastEmailHealthCheckTime { get; private set; }
		public bool LastStorageAccountHealthCheckResult { get; private set; }
		public readonly int StorageAccountHealthCheckInterval = storageAccountHealthCheckInterval;
		public DateTime LastStorageAccountHealthCheckTime { get; private set; }
		public bool LastDatabaseHealthCheckResult { get; private set; }
		public readonly int DatabaseHealthCheckInterval = databaseHealthCheckInterval;
		public DateTime LastDatabaseHealthCheckTime { get; private set; }
		public bool LastContentModerationHealthCheckResult { get; private set; }
		public readonly int ContentModerationHealthCheckInterval = contentModerationHealthCheckInterval;
		public DateTime LastContentModerationHealthCheckTime { get; private set; }
		private readonly Func<IServiceScope> _scopeFactory = scopeFactory;

		private T GetService<T>() where T : notnull {

			var scope = _scopeFactory();
			return scope.ServiceProvider.GetRequiredService<T>();
		}

		public string GetHealthReport()
		{
return $@"
{{
	""database"": {{
		""status"": ""{(LastDatabaseHealthCheckResult ? "Healthy" : "Unhealthy")}"",
		""lastChecked"": ""{LastDatabaseHealthCheckTime}""
	}},
	""storage"": {{
		""status"": ""{(LastStorageAccountHealthCheckResult ? "Healthy" : "Unhealthy")}"",
		""lastChecked"": ""{LastStorageAccountHealthCheckTime}""
	}},
	""email"": {{
		""status"": ""{(LastEmailHealthCheckResult ? "Healthy" : "Unhealthy")}"",
		""lastChecked"": ""{LastEmailHealthCheckTime}""
	}},
	""contentModeration"": {{
		""status"": ""{(LastContentModerationHealthCheckResult ? "Healthy" : "Unhealthy")}"",
		""lastChecked"": ""{LastContentModerationHealthCheckTime}""
	}}
}}
";
		}

		public async Task<bool> IsHealthy()
		{
			if (DateTime.Now.Subtract(LastEmailHealthCheckTime).TotalMilliseconds > EmailHealthCheckInterval)
			{
				LastEmailHealthCheckTime = DateTime.Now;
				var scopedCommunicationService = GetService<CommunicationService>();
				LastEmailHealthCheckResult = await scopedCommunicationService.IsEmailConnectionOk();
			}

			if (DateTime.Now.Subtract(LastStorageAccountHealthCheckTime).TotalMilliseconds > StorageAccountHealthCheckInterval)
			{
				LastStorageAccountHealthCheckTime = DateTime.Now;
				var scopedStorageService = GetService<IMediaElementService>();
				LastStorageAccountHealthCheckResult = await scopedStorageService.IsStorageConnectionOk();
			}

			if (DateTime.Now.Subtract(LastDatabaseHealthCheckTime).TotalMilliseconds > DatabaseHealthCheckInterval)
			{
				LastDatabaseHealthCheckTime = DateTime.Now;
				var scopedDatabaseService = GetService<AppDbContext>();
				LastDatabaseHealthCheckResult = await scopedDatabaseService.Database.CanConnectAsync();

			}

			if (DateTime.Now.Subtract(LastContentModerationHealthCheckTime).TotalMilliseconds > ContentModerationHealthCheckInterval)
			{
				LastContentModerationHealthCheckTime = DateTime.Now;
				var scopedContentModerationService = GetService<IContentModerationService>();
				LastContentModerationHealthCheckResult = await scopedContentModerationService.IsContentModerationServiceOk();
			}

			return LastEmailHealthCheckResult && LastStorageAccountHealthCheckResult && LastDatabaseHealthCheckResult && LastContentModerationHealthCheckResult;

		}
	}
}