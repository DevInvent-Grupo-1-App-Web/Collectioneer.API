using Collectioneer.API.Shared.Domain.Commands;

namespace Collectioneer.API.Shared.Domain.Services
{
	public interface IMediaElementService
	{
		Task<string> UploadMedia(MediaElementUploadCommand request, int uploaderId);
	}
}