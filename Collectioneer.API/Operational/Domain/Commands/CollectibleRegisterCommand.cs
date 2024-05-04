namespace Collectioneer.API.Operational.Domain.Commands
{
    public record CollectibleRegisterCommand
		(
			string Name,
			int CommunityId,
			int OwnerId,
			float? Value
		)
    {
				public int CommunityId { get; init; } = CommunityId;
				public string Name { get; init; } = Name;
        public int OwnerId { get; init; } = OwnerId;
        public float? Value { get; init; } = Value;
    }
}
