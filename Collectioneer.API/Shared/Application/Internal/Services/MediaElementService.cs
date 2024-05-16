using System.Text.RegularExpressions;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Shared.Infrastructure.Configuration;

namespace Collectioneer.API.Shared.Application.Internal.Services;

public class MediaElementService : IMediaElementService
{
	private readonly BlobServiceClient _blobServiceClient;

	public MediaElementService(
		AppKeys appKeys
	)
	{
		_blobServiceClient = new BlobServiceClient(new Uri(appKeys.BlobStorage.URL));
	}
	public async Task<string> UploadMedia(MediaElementUploadCommand request, int uploaderId)
	{
		// Get a reference to a blob container
		var blobContainerClient = _blobServiceClient.GetBlobContainerClient("media");

		// Get a reference to a blob
		var blobClient = blobContainerClient.GetBlobClient($"{uploaderId}/{request.MediaName}");

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