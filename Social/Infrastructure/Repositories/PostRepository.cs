using System.Text.RegularExpressions;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Social.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }

		public async Task<ICollection<Post>> GetByCommunityId(int communityId)
		{
			return await _context.Posts.Where(p => p.CommunityId == communityId).ToListAsync();
		}

		public async Task<ICollection<Post>> Search(string searchTerm)
		{
			searchTerm = Regex.Replace(searchTerm, @"[^a-zA-Z0-9\s]", "");

			var posts = await _context.Posts.Where(p => p.Title.Contains(searchTerm)).ToListAsync();

			var whereContent = await _context.Posts.Where(p => p.Content.Contains(searchTerm)).ToListAsync();

			return posts;
		}
	}
}