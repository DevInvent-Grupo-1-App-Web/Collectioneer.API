using Collectioneer.API.Operational.Domain.Models.Entities;

namespace Collectioneer.API.Social.Domain.Models.Entities;

public class User
{
	public int Id { get; set; }
	public string Username { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public ICollection<Collectible> Collectibles { get; set; } = [];
}