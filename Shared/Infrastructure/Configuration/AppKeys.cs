namespace Collectioneer.API.Shared.Infrastructure.Configuration
{
	public class AppKeys
	{
		public JwtConfig Jwt { get; set; }
		public ContentSafety ContentSafety { get; set; }
		public BlobStorage BlobStorage { get; set; }
		public Persistence Persistence { get; set; }
		public ExternalCommunication ExternalCommunication { get; set; }

		public AppKeys(IConfiguration configuration, ILogger<AppKeys> logger)
		{
			Jwt = new JwtConfig
			{
				Key = configuration["JWT_KEY"] ?? throw new ArgumentNullException("JWT key is missing."),
				Issuer = configuration["JWT_ISSUER"] ?? throw new ArgumentNullException("JWT issuer was not defined."),
				Audience = configuration["JWT_AUDIENCE"] ?? throw new ArgumentNullException("JWT audience was not defined.")
			};

			ContentSafety = new ContentSafety
			{
				Key = configuration["CONTENT_SAFETY_KEY"] ?? throw new ArgumentNullException("Content safety key is missing."),
				Endpoint = configuration["CONTENT_SAFETY_ENDPOINT"] ?? throw new ArgumentNullException("Content safety endpoint was not defined.")
			};

			BlobStorage = new BlobStorage
			{
				URL = configuration["STORAGE_URL"] ?? throw new ArgumentNullException("Storage URL is missing."),
				ConnectionString = configuration["STORAGE_ACCOUNT_CONNECTION_STRING"] ?? throw new ArgumentNullException("Storage account connection string was not defined.")
			};

			Persistence = new Persistence
			{
				ConnectionString = configuration["MYSQL_CONNECTION_STRING"] ?? throw new ArgumentNullException("MySQL connection string was not defined.")
			};

			ExternalCommunication = new ExternalCommunication
			{
				ConnectionString = configuration["COMMUNICATION_SERVICES_CONNECTION_STRING"] ?? throw new ArgumentNullException("Communication services connection string was not defined.")
			};

			if (configuration["ASPNETCORE_ENVIRONMENT"] == "Development")
			{
				logger.LogInformation("JWT:Key: {Key}", Jwt.Key);
				logger.LogInformation("JWT:Issuer: {Issuer}", Jwt.Issuer);
				logger.LogInformation("JWT:Audience: {Audience}", Jwt.Audience);
				logger.LogInformation("CONTENT_SAFETY:Key: {Key}", ContentSafety.Key);
				logger.LogInformation("CONTENT_SAFETY:Endpoint: {Endpoint}", ContentSafety.Endpoint);
				logger.LogInformation("BLOB_STORAGE:URL: {URL}", BlobStorage.URL);
				logger.LogInformation("PERSISTENCE:ConnectionString: {ConnectionString}", Persistence.ConnectionString);
				logger.LogInformation("EXTERNAL_COMMUNICATION:ConnectionString: {ConnectionString}", ExternalCommunication.ConnectionString);
			}
		}
	
		public void CheckKeys()
		{
            if (string.IsNullOrEmpty(Jwt.Key))
			{
                throw new ArgumentNullException("JWT key is missing.");
            }
            if (string.IsNullOrEmpty(Jwt.Issuer))
			{
                throw new ArgumentNullException("JWT issuer was not defined.");
            }
            if (string.IsNullOrEmpty(Jwt.Audience))
			{
                throw new ArgumentNullException("JWT audience was not defined.");
            }
            if (string.IsNullOrEmpty(ContentSafety.Key))
			{
                throw new ArgumentNullException("Content safety key is missing.");
            }
            if (string.IsNullOrEmpty(ContentSafety.Endpoint))
			{
                throw new ArgumentNullException("Content safety endpoint was not defined.");
            }
            if (string.IsNullOrEmpty(BlobStorage.URL))
			{
                throw new ArgumentNullException("Storage URL is missing.");
            }
            if (string.IsNullOrEmpty(BlobStorage.ConnectionString))
			{
                throw new ArgumentNullException("Storage account connection string was not defined.");
            }
            if (string.IsNullOrEmpty(Persistence.ConnectionString))
			{
                throw new ArgumentNullException("MySQL connection string was not defined.");
            }
            if (string.IsNullOrEmpty(ExternalCommunication.ConnectionString))
			{
                throw new ArgumentNullException("Communication services connection string was not defined.");
            }

			Console.WriteLine("All keys are present. Continuing with app startup.");
        }
	}

	public struct JwtConfig
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
	}
	public struct ContentSafety
	{
		public string Key { get; set; }
		public string Endpoint { get; set; }
	}
	public struct BlobStorage
	{
		public string URL { get; set; }
		public string ConnectionString { get; set; }
	}
	public struct Persistence
	{
		public string ConnectionString { get; set; }
	}
	public struct ExternalCommunication
	{
		public string ConnectionString { get; set; }
	}
}