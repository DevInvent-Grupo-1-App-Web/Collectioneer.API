namespace Collectioneer.API.Shared.Domain.Queries
{
    public record UserLoginQuery
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
