namespace Collectioneer.API.Social.Domain.Models.ValueObjects;

public class RoleType
{
    public int Id { get; set; }
    public string Name { get; set; }

    public RoleType(string name)
    {
        Name = name;
    }
}
