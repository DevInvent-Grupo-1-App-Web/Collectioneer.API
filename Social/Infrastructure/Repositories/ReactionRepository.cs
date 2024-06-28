using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Social.Infrastructure.Repositories
{
	public class ReactionRepository(AppDbContext context) : BaseRepository<Reaction>(context), IReactionRepository 
	{
		public async Task<ICollection<Reaction>> GetReactionsByReactableIdAndType(int reactableId, Type reactableType, int userId)
		{
			return await _context.Reactions
				.Where(r => r.UserId == userId)
				.Where(r => reactableType == typeof(Post) && r.PostId == reactableId)
				.Where(r => reactableType == typeof(Comment) && r.CommentId == reactableId)
				.Where(r => reactableType == typeof(Collectible) && r.CollectibleId == reactableId)
				.ToListAsync();
		}
	}
}