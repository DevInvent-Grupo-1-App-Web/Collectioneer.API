using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.Aggregates;

namespace Collectioneer.API.Social.Domain.Models.ValueObjects;

public class Role : ITimestamped
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CommunityId { get; set; }
    public RoleType RoleType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public Community? Community { get; set; }

    public Role(
        int userId,
        int communityId,
        RoleType roleType
    )
    {
        UserId = userId;
        CommunityId = communityId;
        RoleType = roleType;
    }
}
