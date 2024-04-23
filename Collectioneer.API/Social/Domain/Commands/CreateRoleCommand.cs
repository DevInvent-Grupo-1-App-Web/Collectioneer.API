namespace Collectioneer.API.Social.Domain.Commands
{
    public record CreateRoleCommand(
        int UserId,
        int CommunityId,
        string RoleType
    )
    {
        public int UserId { get; init; } = UserId;
        public int CommunityId { get; init; } = CommunityId;
        public string RoleType { get; init; } = RoleType;
    }
}
