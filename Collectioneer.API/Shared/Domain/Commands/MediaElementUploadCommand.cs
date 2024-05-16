namespace Collectioneer.API.Shared.Domain.Commands
{
	public record MediaElementUploadCommand(
		int UploaderId,
		string MediaName,
		string Content,
		string ContentType
	)
	{
		public int UploaderId { get; set; } = UploaderId;
		public string MediaName { get; set; } = MediaName;
		public string Content { get; set; } = Content;
		public string ContentType { get; set; } = ContentType;
	}
}