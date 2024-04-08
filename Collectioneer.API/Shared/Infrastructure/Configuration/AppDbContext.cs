using Microsoft.EntityFrameworkCore;
using Collectioneer.API.Social.Domain.Models.Entities;
using Collectioneer.API.Shared.Infrastructure.Configuration.Extensions;
using Collectioneer.API.Operational.Domain.Models.Entities;

namespace Collectioneer.API.Shared.Infrastructure.Configuration 
{
	public class AppDbContext : DbContext {
		public DbSet<User> Users { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

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
                .HasMaxLength(50);
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
                .Property(x => x.CommunityId)
                .IsRequired();

            modelBuilder.Entity<Collectible>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Collectibles)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

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
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<Article>()
                .HasOne(x => x.Collectible)
                .WithOne(x => x.Article)
                .HasForeignKey<Article>("CollectibleId")
                .OnDelete(DeleteBehavior.Cascade);
            #endregion


            modelBuilder.UseSnakeCaseNamingConvention();
        }

    }
}