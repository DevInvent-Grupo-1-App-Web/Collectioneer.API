using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Collectioneer.API.Shared.Infrastructure.Repositories;

namespace Collectioneer.API.Social.Infrastructure.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(AppDbContext context) : base(context)
		{
		}

		/// <summary>
		/// Returns true if the email already exists, false otherwise.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public async Task<bool> IsEmailUnique(string email)
		{
			return !await _context.Users.AnyAsync(u => u.Email == email);
		}

		/// <summary>
		/// Returns true if the username already exists, false otherwise.
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		public async Task<bool> IsUsernameUnique(string username)
		{
			return !await _context.Users.AnyAsync(u => u.Username == username);
		}

        public async Task<bool> IsValidUser(string username, string hashedPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user != null && user.CheckPassword(hashedPassword);
        }

        public async Task<User?> GetUserData(string username)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
		}
	}
}
