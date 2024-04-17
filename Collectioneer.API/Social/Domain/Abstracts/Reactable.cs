using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Abstracts
{
	public abstract class Reactable
	{
		public ICollection<Reaction> Reactions { get; set; }
	}
}