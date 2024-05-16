using Azure;
using Azure.AI.ContentSafety;

namespace Collectioneer.API.Shared.Application.External.Services;

public class ContentModerationService
{
	private readonly ContentSafetyClient _contentSafetyClient;
    private readonly ILogger<ContentModerationService> _logger;

    public ContentModerationService(IConfiguration configuration, ILogger<ContentModerationService> logger)
    {
        _contentSafetyClient = new ContentSafetyClient(
            new Uri(configuration["CONTENT_SAFETY_ENDPOINT"]),new AzureKeyCredential(configuration["CONTENT_SAFETY_KEY"])
        );
        _logger = logger;
    }

	public async Task<bool> IsImageContentSafe(byte[] content)
	{
		var imageData = new ContentSafetyImageData(BinaryData.FromBytes(content));
		var request = new AnalyzeImageOptions(imageData);

		Response<AnalyzeImageResult> response;
		try
		{
			response = await _contentSafetyClient.AnalyzeImageAsync(request);
		}
		catch (RequestFailedException ex)
		{
			Console.WriteLine("Analyze image failed.\nStatus code: {0}, Error code: {1}, Error message: {2}", ex.Status, ex.ErrorCode, ex.Message);
			throw;
		}

		if (response.Value.CategoriesAnalysis.Any(a => a.Severity > 0.5))
		{
			_logger.LogWarning("Image content is not safe. Suspected inappropriate content detected.");
			return false;
		}
		return true;

	}

	public async Task<bool> IsTextContentSafe(string content)
	{
		var request = new AnalyzeTextOptions(content);

		Response<AnalyzeTextResult> response;
		try
		{
			response = await _contentSafetyClient.AnalyzeTextAsync(request);
		}
		catch (RequestFailedException ex)
		{
			Console.WriteLine("Analyze text failed.\nStatus code: {0}, Error code: {1}, Error message: {2}", ex.Status, ex.ErrorCode, ex.Message);
			throw;
		}

		if (response.Value.CategoriesAnalysis.Any(a => a.Severity > 0.5))
		{
			_logger.LogWarning("Text content is not safe. Suspected inappropriate content detected.");
			return false;
		}
		return true;
	}
}