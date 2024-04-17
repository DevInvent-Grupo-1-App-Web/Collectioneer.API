using Collectioneer.API.Shared.Domain.Interfaces;
using Collectioneer.API.Shared.Domain.Models.Aggregates;

namespace Collectioneer.API.Shared.Domain.Models.Entities
{
	public class MediaElement : ITimestamped
	{
		public int Id { get; set; }
		public int UploaderId { get; set; }
		public string MediaName { get; set; }
		public string MediaURL { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public User Uploader { get; set; }
	}
}