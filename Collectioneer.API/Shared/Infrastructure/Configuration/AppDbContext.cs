using Microsoft.EntityFrameworkCore;
using Collectioneer.API.Shared.Infrastructure.Configuration.Extensions;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Models.Entities;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Models.ValueObjects;

namespace Collectioneer.API.Shared.Infrastructure.Configuration
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<User> Users { get; set; }
		public DbSet<MediaElement> MediaElements { get; set; }
		public DbSet<Auction> Auctions { get; set; }
		public DbSet<Exchange> Exchanges { get; set; }
		public DbSet<Sale> Sales { get; set; }
		public DbSet<Article> Articles { get; set; }
		public DbSet<Collectible> Collectibles { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Bid> Bids { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<RoleType> RoleTypes { get; set; }
		public DbSet<Community> Communities { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<CommentParent> CommentParents { get; set; }
		public DbSet<Filter> Filters { get; set; }
		public DbSet<FilterType> FilterTypes { get; set; }
		public DbSet<PostTag> PostTags { get; set; }
		public DbSet<ReactionReactable> ReactionReactables { get; set; }
		public DbSet<Reaction> Reactions { get; set; }
		public DbSet<ReactionType> ReactionTypes { get; set; }
		public DbSet<Tag> Tags { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			AuctionModelBuilder(modelBuilder);
			ExchangeModelBuilder(modelBuilder);
			SaleModelBuilder(modelBuilder);
			ArticleModelBuilder(modelBuilder);
			CollectibleModelBuilder(modelBuilder);
			ReviewModelBuilder(modelBuilder);
			BidModelBuilder(modelBuilder);
			RoleModelBuilder(modelBuilder);
			RoleTypeModelBuilder(modelBuilder);
			UserModelBuilder(modelBuilder);
			MediaElementModelBuilder(modelBuilder);
			CommunityModelBuilder(modelBuilder);
			PostModelBuilder(modelBuilder);
			CommentModelBuilder(modelBuilder);
			CommentParentModelBuilder(modelBuilder);
			FilterModelBuilder(modelBuilder);
			FilterTypeModelBuilder(modelBuilder);
			PostTagModelBuilder(modelBuilder);
			ReactionModelBuilder(modelBuilder);
			ReactionReactableModelBuilder(modelBuilder);
			ReactionTypeModelBuilder(modelBuilder);
			TagModelBuilder(modelBuilder);

			RelationshipBuilder(modelBuilder);

			modelBuilder.UseSnakeCaseNamingConvention();
		}

		private static void AuctionModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Auction>()
					.ToTable("Auctions");

			modelBuilder.Entity<Auction>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Auction>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Auction>()
					.Property(x => x.AuctioneerId)
					.IsRequired();

			modelBuilder.Entity<Auction>()
					.Property(x => x.CollectibleId)
					.IsRequired();

			modelBuilder.Entity<Auction>()
					.Property(x => x.StartingPrice)
					.IsRequired();

			modelBuilder.Entity<Auction>()
					.Property(x => x.Deadline)
					.IsRequired();
			
			modelBuilder.Entity<Auction>()
					.Property(x => x.IsOpen)
					.IsRequired()
					.HasDefaultValue(true);

			modelBuilder.Entity<Auction>()
					.Property(x => x.AuctioneerHasCollected)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Auction>()
					.Property(x => x.BidderHasCollected)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Auction>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Auction>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedOnAddOrUpdate();
		}
		private static void ExchangeModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Exchange>()
					.ToTable("Exchanges");

			modelBuilder.Entity<Exchange>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Exchange>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Exchange>()
					.Property(x => x.CommunityId)
					.IsRequired();

			modelBuilder.Entity<Exchange>()
					.Property(x => x.ExchangerId)
					.IsRequired();

			modelBuilder.Entity<Exchange>()
					.Property(x => x.CollectibleId)
					.IsRequired();

			modelBuilder.Entity<Exchange>()
					.Property(x => x.Price)
					.IsRequired();

			modelBuilder.Entity<Exchange>()
					.Property(x => x.IsOpen)
					.IsRequired()
					.HasDefaultValue(true);

			modelBuilder.Entity<Exchange>()
					.Property(x => x.ExchangerHasConfirmed)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Exchange>()
					.Property(x => x.AcceptedExchangeId);

			modelBuilder.Entity<Exchange>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Exchange>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedOnAddOrUpdate();
		}
		private static void SaleModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Sale>()
					.ToTable("Sales");

			modelBuilder.Entity<Sale>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Sale>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Sale>()
					.Property(x => x.VendorId)
					.IsRequired();

			modelBuilder.Entity<Sale>()
					.Property(x => x.CollectibleId)
					.IsRequired();

			modelBuilder.Entity<Sale>()
					.Property(x => x.BuyerId);

			modelBuilder.Entity<Sale>()
					.Property(x => x.Price)
					.IsRequired();

			modelBuilder.Entity<Sale>()
					.Property(x => x.IsOpen)
					.IsRequired()
					.HasDefaultValue(true);

			modelBuilder.Entity<Sale>()
					.Property(x => x.VendorHasCollected)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Sale>()
					.Property(x => x.BuyerHasCollected)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Sale>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Sale>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedOnAddOrUpdate();
		}
		private static void ArticleModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Article>()
								.ToTable("Articles");

			modelBuilder.Entity<Article>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Article>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Article>()
					.Property(x => x.CollectibleId)
					.IsRequired();

			modelBuilder.Entity<Article>()
					.Property(x => x.Title)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<Article>()
					.Property(x => x.Content)
					.HasMaxLength(500);

			modelBuilder.Entity<Article>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Article>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedOnAddOrUpdate();
		}
		private static void CollectibleModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Collectible>()
					.ToTable("Collectibles");

			modelBuilder.Entity<Collectible>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Collectible>()
					.Property(x => x.CommunityId)
					.IsRequired();

			modelBuilder.Entity<Collectible>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.OwnerId)
					.IsRequired();

			modelBuilder.Entity<Collectible>()
					.Property(x => x.Value);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.ArticleId);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.AuctionId);
			
			modelBuilder.Entity<Collectible>()
					.Property(x => x.SaleId);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.ExchangeId);

			modelBuilder.Entity<Collectible>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Collectible>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedOnAddOrUpdate();
		}
		private static void ReviewModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Review>()
								.ToTable("Reviews");

			modelBuilder.Entity<Review>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Review>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Review>()
					.Property(x => x.ReviewerId)
					.IsRequired();

			modelBuilder.Entity<Review>()
					.Property(x => x.ArticleId)
					.IsRequired();

			modelBuilder.Entity<Review>()
					.Property(x => x.Content);

			modelBuilder.Entity<Review>()
					.Property(x => x.Rating)
					.IsRequired();

			modelBuilder.Entity<Review>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Review>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedOnAddOrUpdate();
		}
		private static void BidModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Bid>()
					.ToTable("Bids");

			modelBuilder.Entity<Bid>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Bid>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Bid>()
					.Property(x => x.AuctionId)
					.IsRequired();

			modelBuilder.Entity<Bid>()
					.Property(x => x.BidderId)
					.IsRequired();

			modelBuilder.Entity<Bid>()
					.Property(x => x.Amount)
					.IsRequired();

			modelBuilder.Entity<Bid>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Bid>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedOnAddOrUpdate();
		}
		private static void RoleModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Role>()
					.ToTable("Roles");

			modelBuilder.Entity<Role>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Role>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Role>()
					.Property(x => x.UserId)
					.IsRequired();

			modelBuilder.Entity<Role>()
					.Property(x => x.CommunityId)
					.IsRequired();

			modelBuilder.Entity<Role>()
					.Property(x => x.RoleType)
					.IsRequired();

			modelBuilder.Entity<Role>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Role>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedOnAddOrUpdate();
		}
		private static void RoleTypeModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<RoleType>()
					.ToTable("RoleTypes");

			modelBuilder.Entity<RoleType>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<RoleType>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<RoleType>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);
		}
		private static void UserModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
					.ToTable("Users");

			modelBuilder.Entity<User>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<User>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<User>()
					.Property(x => x.Username)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<User>()
					.Property(x => x.Email)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<User>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<User>()
					.Property(x => x.Password)
					.IsRequired()
					.HasMaxLength(64);
		}
		private static void MediaElementModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<MediaElement>()
					.ToTable("MediaElement");

			modelBuilder.Entity<MediaElement>()
			.HasKey(x => x.Id);

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.UploaderId)
					.IsRequired();

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.MediaName)
					.IsRequired()
					.HasMaxLength(256);

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.MediaURL)
					.IsRequired()
					.HasMaxLength(256);

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<MediaElement>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedOnAddOrUpdate();
		}
		private static void CommunityModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Community>()
					.ToTable("Communities");

			modelBuilder.Entity<Community>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Community>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Community>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<Community>()
					.Property(x => x.Description)
					.HasMaxLength(500);
		}
		private static void PostModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Post>()
					.ToTable("Posts");

			modelBuilder.Entity<Post>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Post>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Post>()
					.Property(x => x.CommunityId)
					.IsRequired();

			modelBuilder.Entity<Post>()
					.Property(x => x.Title)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<Post>()
					.Property(x => x.Content)
					.IsRequired()
					.HasMaxLength(500);

			modelBuilder.Entity<Post>()
					.Property(x => x.AuthorId)
					.IsRequired();

			modelBuilder.Entity<Post>()
					.Property(x => x.IsHidden)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Post>()
					.Property(x => x.IsArchived)
					.IsRequired()
					.HasDefaultValue(false);

			modelBuilder.Entity<Post>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Post>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedOnAddOrUpdate();
		}
		private static void CommentModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Comment>()
					.ToTable("Comments");

			modelBuilder.Entity<Comment>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Comment>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();
			
			modelBuilder.Entity<Comment>()
					.Property(x => x.CommentParentId)
					.IsRequired();
			
			modelBuilder.Entity<Comment>()
					.Property(x => x.UserId)
					.IsRequired();

			modelBuilder.Entity<Comment>()
					.Property(x => x.Content)
					.IsRequired()
					.HasMaxLength(1024);

			modelBuilder.Entity<Comment>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Comment>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedOnAddOrUpdate();
				
			modelBuilder.Entity<Comment>()
					.Property(x => x.IsHidden)
					.IsRequired()
					.HasDefaultValue(false);
		}
		private static void CommentParentModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CommentParent>()
					.ToTable("CommentParents");

			modelBuilder.Entity<CommentParent>()
					.HasKey(x => x.Id);
				
			modelBuilder.Entity<CommentParent>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<CommentParent>()
					.Property(x => x.CommentId)
					.IsRequired();

			modelBuilder.Entity<CommentParent>()
					.Property(x => x.ParentId)
					.IsRequired();

			modelBuilder.Entity<CommentParent>()
					.Property(x => x.ParentType)
					.IsRequired();
		}
		private static void FilterModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Filter>()
					.ToTable("Filters");

			modelBuilder.Entity<Filter>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Filter>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Filter>()
					.Property(x => x.CommunityId)
					.IsRequired();

			modelBuilder.Entity<Filter>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<Filter>()
					.Property(x => x.Type)
					.IsRequired();
		}
		private static void FilterTypeModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<FilterType>()
					.ToTable("FilterTypes");

			modelBuilder.Entity<FilterType>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<FilterType>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<FilterType>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);
		}
		private static void PostTagModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostTag>()
					.ToTable("PostTags");
			
			modelBuilder.Entity<PostTag>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<PostTag>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();
				
			modelBuilder.Entity<PostTag>()
					.Property(x => x.PostId)
					.IsRequired();

			modelBuilder.Entity<PostTag>()
					.Property(x => x.TagId)
					.IsRequired();
		}
		private static void ReactionModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Reaction>()
					.ToTable("Reactions");

			modelBuilder.Entity<Reaction>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Reaction>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();
					
			modelBuilder.Entity<Reaction>()
					.Property(x => x.ReactionReactableId)
					.IsRequired();

			modelBuilder.Entity<Reaction>()
					.Property(x => x.UserId)
					.IsRequired();

			modelBuilder.Entity<Reaction>()
					.Property(x => x.Type)
					.IsRequired();

			modelBuilder.Entity<Reaction>()
					.Property(x => x.CreatedAt)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Reaction>()
					.Property(x => x.UpdatedAt)
					.IsRequired()
					.ValueGeneratedOnAddOrUpdate();
		}
		private static void ReactionReactableModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ReactionReactable>()
					.ToTable("ReactionReactables");

			modelBuilder.Entity<ReactionReactable>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<ReactionReactable>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<ReactionReactable>()
					.Property(x => x.ReactionId)
					.IsRequired();

			modelBuilder.Entity<ReactionReactable>()
					.Property(x => x.ReactableId)
					.IsRequired();

			modelBuilder.Entity<ReactionReactable>()
					.Property(x => x.ReactableType)
					.IsRequired();
		}
		private static void ReactionTypeModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ReactionType>()
					.ToTable("ReactionTypes");

			modelBuilder.Entity<ReactionType>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<ReactionType>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<ReactionType>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);
		}
		private static void TagModelBuilder(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Tag>()
					.ToTable("Tags");

			modelBuilder.Entity<Tag>()
					.HasKey(x => x.Id);

			modelBuilder.Entity<Tag>()
					.Property(x => x.Id)
					.IsRequired()
					.ValueGeneratedOnAdd();

			modelBuilder.Entity<Tag>()
					.Property(x => x.CommunityId)
					.IsRequired();
				
			modelBuilder.Entity<Tag>()
					.Property(x => x.Name)
					.IsRequired()
					.HasMaxLength(50);

			modelBuilder.Entity<Tag>()
					.Property(x => x.Value)
					.IsRequired()
					.HasMaxLength(50);
		}
		private static void RelationshipBuilder(ModelBuilder modelBuilder)
		{
			// One auction has one auctioneer
			modelBuilder.Entity<Auction>().
					HasOne(x => x.Auctioneer)
					.WithMany(x => x.Auctions)
					.HasForeignKey(x => x.AuctioneerId)
					.OnDelete(DeleteBehavior.Restrict);

			// One auction has one collectible
			modelBuilder.Entity<Auction>()
					.HasOne(x => x.Collectible)
					.WithOne(x => x.Auction)
					.HasForeignKey<Auction>("CollectibleId")
					.OnDelete(DeleteBehavior.Restrict);

			// One auction has many bids
			modelBuilder.Entity<Auction>()
					.HasMany(x => x.Bids)
					.WithOne(x => x.Auction)
					.HasForeignKey(x => x.AuctionId)
					.OnDelete(DeleteBehavior.Cascade);

			// One collectible has one article
			modelBuilder.Entity<Article>()
					.HasOne(x => x.Collectible)
					.WithOne(x => x.Article)
					.HasForeignKey<Article>("CollectibleId")
					.OnDelete(DeleteBehavior.Cascade);

			// Many collectibles have one owner
			modelBuilder.Entity<Collectible>()
					.HasOne(x => x.Owner)
					.WithMany(x => x.Collectibles)
					.HasForeignKey(x => x.OwnerId)
					.OnDelete(DeleteBehavior.Cascade);

			// One collectible has one article
			modelBuilder.Entity<Collectible>()
					.HasOne(x => x.Article)
					.WithOne(x => x.Collectible)
					.HasForeignKey<Collectible>("ArticleId")
					.OnDelete(DeleteBehavior.Restrict);

			// One collectible has zero or one auction
			modelBuilder.Entity<Collectible>()
					.HasOne(c => c.Auction)
					.WithOne(a => a.Collectible)
					.HasForeignKey<Auction>(a => a.CollectibleId)
					.OnDelete(DeleteBehavior.Restrict);

			// One reviewer has many reviews
			modelBuilder.Entity<Review>()
					.HasOne(x => x.Reviewer)
					.WithMany(x => x.Reviews)
					.HasForeignKey(x => x.ReviewerId)
					.OnDelete(DeleteBehavior.Restrict);

			// One review has one reviewed item
			modelBuilder.Entity<Review>()
					.HasOne(x => x.ReviewedArticle)
					.WithMany(x => x.Reviews)
					.HasForeignKey(x => x.ArticleId)
					.OnDelete(DeleteBehavior.Restrict);

			// One auction has many bids
			modelBuilder.Entity<Bid>()
					.HasOne(x => x.Auction)
					.WithMany(x => x.Bids)
					.HasForeignKey(x => x.AuctionId)
					.OnDelete(DeleteBehavior.Cascade);


			// One bid has one bidder
			modelBuilder.Entity<Bid>()
					.HasOne(x => x.Bidder)
					.WithMany(x => x.Bids)
					.HasForeignKey(x => x.BidderId)
					.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Role>()
					.HasOne(x => x.User)
					.WithMany(x => x.Roles)
					.HasForeignKey(x => x.UserId)
					.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Role>()
					.HasOne(x => x.Community)
					.WithMany(x => x.Roles)
					.HasForeignKey(x => x.CommunityId)
					.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<User>()
					.HasMany(x => x.Collectibles)
					.WithOne(x => x.Owner)
					.HasForeignKey(x => x.OwnerId)
					.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<User>()
					.HasMany(x => x.Auctions)
					.WithOne(x => x.Auctioneer)
					.HasForeignKey(x => x.AuctioneerId)
					.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<User>()
					.HasMany(x => x.Bids)
					.WithOne(x => x.Bidder)
					.HasForeignKey(x => x.BidderId)
					.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<MediaElement>()
					.HasOne(x => x.Uploader)
					.WithMany(x => x.MediaElements)
					.HasForeignKey(x => x.UploaderId)
					.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Community>()
					.HasMany(x => x.Roles)
					.WithOne(x => x.Community)
					.HasForeignKey(x => x.CommunityId)
					.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Community>()
					.HasMany(x => x.CommunityFilters)
					.WithOne(x => x.Community)
					.HasForeignKey(x => x.CommunityId)
					.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Community>()
					.HasMany(x => x.CommunityTags)
					.WithOne(x => x.Community)
					.HasForeignKey(x => x.CommunityId)
					.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Reaction>()
					.HasOne(x => x.User)
					.WithMany(x => x.Reactions)
					.HasForeignKey(x => x.UserId)
					.OnDelete(DeleteBehavior.Cascade);
		}
	}
}