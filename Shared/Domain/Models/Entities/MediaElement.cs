using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.Aggregates;

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
		public int? CollectibleId { get; set; }
		public int? PostId { get; set; }
		public int? ProfileId { get; set; }
		public int? CommunityId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public User? Uploader { get; set; }
		public Collectible? Collectible { get; set; }
		public Community? Community { get; set; }
		public Post? Post { get; set; }
		public User? Profile { get; set; }

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