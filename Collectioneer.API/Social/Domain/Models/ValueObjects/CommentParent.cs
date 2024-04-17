namespace Collectioneer.API.Social.Domain.Models.ValueObjects
{
	public class CommentParent
	{
		public int Id { get; set; }
		public int CommentId { get; set; }
		public int ParentId { get; set; }
		public CommentParentType ParentType { get; set; }

		public CommentParent(
			int commentId,
			int parentId,
			CommentParentType parentType
		)
		{
			CommentId = commentId;
			ParentId = parentId;
			ParentType = parentType;
		}
	}
}