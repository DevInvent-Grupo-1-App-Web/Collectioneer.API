using System.Text.RegularExpressions;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Services;

namespace Collectioneer.API.Shared.Application.Internal.Services;

public class MediaElementService : IMediaElementService
{
	private readonly BlobServiceClient _blobServiceClient;
	private readonly IConfiguration _configuration;

	public MediaElementService(IConfiguration configuration)
	{
		_configuration = configuration;
		_blobServiceClient = new BlobServiceClient(new Uri($"{_configuration["STORAGE_URL"]}"));
	}
	public async Task<string> UploadMedia(MediaElementUploadCommand request)
	{
		// Get a reference to a blob container
		var blobContainerClient = _blobServiceClient.GetBlobContainerClient("media");

		// Get a reference to a blob
		var blobClient = blobContainerClient.GetBlobClient($"{request.UploaderId}/{request.MediaName}");

		// Upload the file to the blob
		using var stream = new MemoryStream(Convert.FromBase64String(request.Content), writable: false);
		await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = request.ContentType });

		string url = blobClient.Uri.ToString();

		Match match = Regex.Match(url, @"^(.*\/[^?]*)");
		if (match.Success)
		{
			url = match.Groups[1].Value;
		}

		return url;
	}
}