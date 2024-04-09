namespace Collectioneer.API.Social.Domain.Commands
{
    public record UserRegisterCommand
    {
        public string Email { get; init; }
        public string Name { get; init; }
        public string Password { get; init; }
        public string Username { get; init; }
    }
}
