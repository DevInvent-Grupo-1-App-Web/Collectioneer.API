using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Social.Domain.Models.Aggregates;

public class Community
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string CoverImage { get; set; }
	public ICollection<Role> Roles { get; set; }
	public ICollection<Filter> CommunityFilters { get; set; }
	public ICollection<Tag> CommunityTags { get; set; }
}
