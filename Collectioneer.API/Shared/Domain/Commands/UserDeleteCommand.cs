namespace Collectioneer.API.Shared.Domain.Commands
{
    public record UserDeleteCommand
    {
        public string Password { get; init; }
        public string Username { get; init; }
    }
}
