namespace Collectioneer.API.Social.Domain.Models.ValueObjects;

public class ReactionReactable
{
	public int Id { get; set; }
	public int ReactionId { get; set; }
	public int ReactableId { get; set; }
	public ReactableType ReactableType { get; set; }

	// Navigation properties

	public ReactionReactable(
		int reactionId,
		int reactableId,
		ReactableType reactableType
	)
	{
		ReactionId = reactionId;
		ReactableId = reactableId;
		ReactableType = reactableType;
	}
}
