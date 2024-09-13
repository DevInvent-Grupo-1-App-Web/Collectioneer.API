namespace Collectioneer.API.Shared.Domain.Commands
{
    public record UserUpdateCommand
    {
        public string? Username { get; init; }
        public string? Email { get; init; }
        public string? Name { get; init; }
    }
}