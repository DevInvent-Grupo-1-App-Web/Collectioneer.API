namespace Collectioneer.API.Operational.Domain.Models.ValueObjects;

public class Role
{
	public int Id {get; private set;}
	public int UserId {get; private set;}
	public int CommunityId {get; private set;}
	public RoleType RoleType {get; private set;}
}
