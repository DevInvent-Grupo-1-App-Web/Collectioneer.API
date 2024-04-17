using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;

namespace Collectioneer.API.Shared.Domain.Models.Entities
{
	public class MediaElement : ITimestamped
	{
		// Entity identifier
		public int Id { get; set; }
		// Entity properties
		public int UploaderId { get; set; }
		public string MediaName { get; set; }
		public string MediaURL { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public User? Uploader { get; set; }

		public MediaElement(
			int uploaderId,
			string mediaName,
			string mediaURL
		)
		{
			UploaderId = uploaderId;
			MediaName = mediaName;
			MediaURL = mediaURL;
		}
	}
}