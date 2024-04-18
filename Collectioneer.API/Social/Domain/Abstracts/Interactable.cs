using Collectioneer.API.Social.Domain.Interfaces;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Abstracts
{
	public abstract class Interactable : ICommentable, IReactable
	{
		public ICollection<Comment> Comments { get; set; } = [];
		public ICollection<Reaction> Reactions { get; set; } = [];
	}
}