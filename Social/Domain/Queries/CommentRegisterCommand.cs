namespace Collectioneer.API.Social.Domain.Queries
{
	public record CommentRegisterCommand(
		int AuthorId,
		string Content
	)
	{
		public int AuthorId { get; init; } = AuthorId;
		public int? PostId { get; set; }
		public int? CommentId { get; set; }
		public int? CollectibleId { get; set; }
		public int? ReviewId { get; set; }
		public string Content { get; init; } = Content;
	}
}