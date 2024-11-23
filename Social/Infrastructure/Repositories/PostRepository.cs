using System.Text.RegularExpressions;
using Collectioneer.API.Operational.Application.External;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Social.Infrastructure.Repositories
{
    public class PostRepository(AppDbContext context) : BaseRepository<Post>(context), IPostRepository
    {
		public async Task<ICollection<Post>> GetPosts(int communityId)
		{
			return await _context.Posts
				.Include(p => p.Author)
				.Include(p => p.MediaElements)
				.Include(p => p.Community)
				.Where(p => p.CommunityId == communityId)
				.ToListAsync();
		}

		public async Task<PaginatedResult<Post>> Search(string searchTerm, int communityId, int page, int pageSize)
		{
            searchTerm = Regex.Replace(searchTerm, @"[^a-zA-Z0-9\s]", "");

            var query = _context.Posts
	            .Include(p => p.Author)
				.Include(p => p.MediaElements)
				.Include(p => p.Community)
                .Where(p => p.CommunityId == communityId && (p.Title.Contains(searchTerm) || p.Content.Contains(searchTerm)));
            
            var totalItems = await query.CountAsync();
            var items = await query
	            .Skip((page - 1) * pageSize)
	            .Take(pageSize)
	            .ToListAsync();
            
            return new PaginatedResult<Post>
            {
	            Items = items,
	            CurrentPage = page,
	            PageSize = pageSize,
	            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
		}
	}
}