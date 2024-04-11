namespace Collectioneer.API.Social.Domain.Commands
{
    public record UserDeleteCommand
    {
        public string Password { get; init; }
        public string Username { get; init; }
    }
}
