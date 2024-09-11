﻿using Collectioneer.API.Shared.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Repositories;

namespace Collectioneer.API.Shared.Infrastructure.Repositories
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

		public async Task<User?> GetUserByUsername(string username)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
		}

		public async Task<User?> GetUserByEmail(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}
		
		// public async Task<User?> GetUsersByName(string name) //New code added 11-09-2024
		// {
		// 	return await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
		// }
		public async Task<List<User>> GetUsersByName(string name)
		{
			return await _context.Users.Where(u => u.Name == name).ToListAsync();
		}

	}
}
