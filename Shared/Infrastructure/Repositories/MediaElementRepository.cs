using Collectioneer.API.Shared.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Shared.Infrastructure.Repositories;

class MediaElementRepository : BaseRepository<MediaElement>, IMediaElementRepository
{
	public MediaElementRepository(AppDbContext context) : base(context)
	{
	}

	public async Task<ICollection<MediaElement>> GetMediaElementsByCollectibleId(int collectibleId)
	{
		var mediaElements = await _context.MediaElements
			.Where(m => m.CollectibleId == collectibleId).ToListAsync();

		return mediaElements;
		
	}

	public async Task<ICollection<MediaElement>> GetMediaElementsByCommunityId(int communityId)
	{
		var mediaElements = await _context.MediaElements
			.Where(m => m.CommunityId == communityId).ToListAsync();

		return mediaElements;
	}

	public async Task<ICollection<MediaElement>> GetMediaElementsByPostId(int postId)
	{
		var mediaElements = await _context.MediaElements
			.Where(m => m.PostId == postId).ToListAsync();
		
		return mediaElements;
	}

	public async Task<ICollection<MediaElement>> GetMediaElementsByProfileId(int profileId)
	{
		var mediaElements = await _context.MediaElements
			.Where(m => m.ProfileId == profileId).ToListAsync();

		return mediaElements;
	}
}