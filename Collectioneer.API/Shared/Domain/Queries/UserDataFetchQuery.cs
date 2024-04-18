namespace Collectioneer.API.Shared.Domain.Queries
{
    public record UserDataFetchQuery(
				string Username
		)
    {
        public string Username { get; init; } = Username;
    }
}
