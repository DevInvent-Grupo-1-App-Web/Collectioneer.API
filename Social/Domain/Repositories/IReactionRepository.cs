using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Repositories
{
	public interface IReactionRepository : IBaseRepository<Reaction>
	{
		public Task<ICollection<Reaction>> GetReactionsByReactableIdAndType(int reactableId, Type reactableType, int userId);
	}
}