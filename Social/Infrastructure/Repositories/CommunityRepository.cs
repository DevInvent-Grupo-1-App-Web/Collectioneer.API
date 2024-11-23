using System.Text.RegularExpressions;
using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Shared.Infrastructure.Repositories;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Collectioneer.API.Social.Infrastructure.Repositories
{
    public class CommunityRepository(AppDbContext context) : BaseRepository<Community>(context), ICommunityRepository
    {
		public async Task<ICollection<Community>> Search(string searchTerm)
        {
            searchTerm = Regex.Replace(searchTerm, @"[^a-zA-Z0-9\s]", "");

			var communities = await this._context.Communities
				.Where(c => c.Name.Contains(searchTerm))
				.Take(50)
				.ToListAsync();

            return communities;
        }

        public Task<bool> Delete(int id)
        {
            var comunity = this._context.Communities.Find(id);
            
            if (comunity == null)
            {
                return Task.FromResult(false);
            }else {
                this._context.Communities.Remove(comunity);
                return Task.FromResult(true);
            }
        }

		public new async Task<IEnumerable<Community>> GetAll()
		{
			return await _context.Set<Community>().Take(50).ToListAsync();
		}
    }
}
