namespace Collectioneer.API.Shared.Domain.Services
{
	public interface IContentModerationService
	{
		Task<bool> ScreenTextContent(string content);

		Task<bool> IsContentModerationServiceOk();
	}
}