using Microsoft.EntityFrameworkCore;
using Collectioneer.API.Social.Domain.Models.Entities;
using Collectioneer.API.Shared.Infrastructure.Configuration.Extensions;

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

            modelBuilder.UseSnakeCaseNamingConvention();
        }

    }
}