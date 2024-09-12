using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Application.External
{
	public class CommunityDTO
	{
		public int Id { get; init; }
		public string Name { get; init; }
		public string Description { get; init; }

		// Constructor
		public CommunityDTO(Community community)
		{
			Id = community.Id;
			Name = community.Name;
			Description = community.Description;
		}
	}
}
