using Azure;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.Azure.CognitiveServices.ContentModerator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Collectioneer.API.Shared.Application.External.Services;

public class ContentModerationService : IContentModerationService
{
	private readonly ContentModeratorClient _contentModeratorClient;
    private readonly ILogger<ContentModerationService> _logger;

	public static ContentModeratorClient Authenticate(string key, string endpoint) {
		ContentModeratorClient client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(key))
		{
			Endpoint = endpoint
		};

		return client;
	}

    public ContentModerationService(AppKeys appKeys, ILogger<ContentModerationService> logger)
    {
        _contentModeratorClient = Authenticate(appKeys.ContentSafety.Key, appKeys.ContentSafety.Endpoint);
        _logger = logger;
    }

	public async Task<bool> ScreenTextContent(string content)
	{
		var text = Encoding.UTF8.GetBytes(content);
		MemoryStream stream = new(text);

		var screenResult = await _contentModeratorClient.TextModeration.ScreenTextAsync("text/plain", stream, null, true, true, null, true, CancellationToken.None);

		Console.WriteLine($"{JsonConvert.SerializeObject(screenResult)}");

		if (GetScore(screenResult) > 0.5 || screenResult.Classification?.ReviewRecommended == true)
		{
			_logger.LogInformation($"Review recommended for text: {content}");
			return false;
		}
		
		return true;
	}

	private static double GetScore(Screen screen)
	{
		if (!screen.Terms.IsNullOrEmpty())
		{
			return 1.0d;
		}

		if (screen == null || screen.Classification == null)
		{
			return 0.0d;
		}

		var explicitLanguage = screen.Classification.Category1?.Score ?? 0.0d;
		var suggestiveLanguage = screen.Classification.Category2?.Score ?? 0.0d;
		var offensiveLanguage = screen.Classification.Category3?.Score ?? 0.0d;
	
		return (explicitLanguage + suggestiveLanguage + offensiveLanguage) / 3;
	}

}