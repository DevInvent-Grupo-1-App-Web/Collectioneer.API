using Microsoft.EntityFrameworkCore;
using Collectioneer.API.Shared.Infrastructure.Configuration.Extensions;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Shared.Domain.Models.Aggregates;

namespace Collectioneer.API.Shared.Infrastructure.Configuration
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
		public DbSet<User> Users { get; set; }
        public DbSet<Collectible> Collectibles { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);

            #region User
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

            #endregion

            #region Collectible
            modelBuilder.Entity<Collectible>()
                .ToTable("Collectibles");

            modelBuilder.Entity<Collectible>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Collectible>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

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
                .Property(x=>x.ArticleId);

            modelBuilder.Entity<Collectible>()
                .Property(x => x.AuctionId);


            modelBuilder.Entity<Collectible>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Collectibles)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Collectible>()
                .HasOne(x => x.Article)
                .WithOne(x => x.Collectible)
                .HasForeignKey<Collectible>("ArticleId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Collectible>()
                .HasOne(c => c.Auction)
                .WithOne(a => a.Collectible)
                .HasForeignKey<Auction>(a => a.CollectibleId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Article
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
                .HasOne(x => x.Collectible)
                .WithOne(x => x.Article)
                .HasForeignKey<Article>("CollectibleId")
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Auction
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
                .Property(x => x.CurrentPrice)
                .IsRequired();

            modelBuilder.Entity<Auction>()
                .Property(x => x.Deadline)
                .IsRequired();

            modelBuilder.Entity<Auction>().
                HasOne(x => x.Auctioneer)
                .WithMany(x => x.Auctions)
                .HasForeignKey(x => x.AuctioneerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Auction>()
                .HasOne(x => x.Collectible)
                .WithOne(x => x.Auction)
                .HasForeignKey<Auction>("CollectibleId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Auction>()
                .HasMany(x => x.Bids)
                .WithOne(x => x.Auction)
                .HasForeignKey(x => x.AuctionId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Bid
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
                .Property(x => x.Time)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Bid>()
                .HasOne(x => x.Auction)
                .WithMany(x => x.Bids)
                .HasForeignKey(x => x.AuctionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bid>()
                .HasOne(x => x.Bidder)
                .WithMany(x => x.Bids)
                .HasForeignKey(x => x.BidderId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            modelBuilder.UseSnakeCaseNamingConvention();
        }

    }
}