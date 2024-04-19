using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Models.Aggregates;

public class Community
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }

	// Navigation properties
	public ICollection<Collectible> Collectibles { get; set; } = [];
	public ICollection<Post> Posts { get; set; } = [];
	public ICollection<Auction> Auctions { get; set; } = [];
	public ICollection<Exchange> Exchanges { get; set; } = [];
	public ICollection<Sale> Sales { get; set; } = [];
	public ICollection<Role> Roles { get; set; } = [];
	public ICollection<Filter> CommunityFilters { get; set; } = [];
	public ICollection<Tag> CommunityTags { get; set; } = [];

	public Community(
		string name,
		string description
	)
	{
		Name = name;
		Description = description;
	}
}
