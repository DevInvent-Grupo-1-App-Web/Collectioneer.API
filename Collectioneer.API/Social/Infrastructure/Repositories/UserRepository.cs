using Collectioneer.API.Shared.Infrastructure.Configuration;
using Collectioneer.API.Social.Domain.Models.Entities;
using Collectioneer.API.Social.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using PhoneResQ.API.Shared.Infrastructure.Repositories;

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
            return !await _context.Set<User>().AnyAsync(u => u.Email == email);
        }

        /// <summary>
        /// Returns true if the username already exists, false otherwise.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> IsUsernameUnique(string username)
        {
            return !await _context.Set<User>().AnyAsync(u => u.Username == username);
        }

        public async Task<bool> IsValidUser(string username, string password)
        {
            return await _context.Set<User>().AnyAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User?> GetUserData(string username)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
