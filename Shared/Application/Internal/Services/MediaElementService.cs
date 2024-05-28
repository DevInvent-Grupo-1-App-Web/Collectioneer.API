using System.Text.RegularExpressions;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Services;
using Collectioneer.API.Shared.Infrastructure.Configuration;

namespace Collectioneer.API.Shared.Application.Internal.Services;

public class MediaElementService : IMediaElementService
{
	private readonly BlobServiceClient _blobServiceClient;
	private readonly IMediaElementRepository _mediaElementRepository;
	private readonly IUnitOfWork _unitOfWork;

	public MediaElementService(
		AppKeys appKeys,
		IMediaElementRepository mediaElementRepository,
		IUnitOfWork unitOfWork
	)
	{
		_blobServiceClient = new BlobServiceClient(new Uri(appKeys.BlobStorage.URL));
		_mediaElementRepository = mediaElementRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<ICollection<MediaElement>> GetMediaElementsByCollectibleId(int collectibleId)
	{
		return await _mediaElementRepository.GetMediaElementsByCollectibleId(collectibleId);
	}

	public Task<ICollection<MediaElement>> GetMediaElementsByCommunityId(int communityId)
	{
		return _mediaElementRepository.GetMediaElementsByCommunityId(communityId);
	}

	public Task<ICollection<MediaElement>> GetMediaElementsByPostId(int postId)
	{
		return _mediaElementRepository.GetMediaElementsByPostId(postId);
	}

	public Task<ICollection<MediaElement>> GetMediaElementsByProfileId(int profileId)
	{
		return _mediaElementRepository.GetMediaElementsByProfileId(profileId);
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

		// Create a new media element
		var mediaElement = new MediaElement
		(uploaderId, request.MediaName, url);

		switch (request.AttachedToType)
		{
			case "collectible":
				mediaElement.CollectibleId = request.AttachedToId;
				break;
			case "community":
				mediaElement.CommunityId = request.AttachedToId;
				break;
			case "post":
				mediaElement.PostId = request.AttachedToId;
				break;
			case "profile":
				mediaElement.ProfileId = request.AttachedToId;
				break;

			default:
				throw new ArgumentException("Invalid attached to type");
		}

		// Save the media element to the database
		await _mediaElementRepository.Add(mediaElement);
		await _unitOfWork.CompleteAsync();

		return url;
	}
}