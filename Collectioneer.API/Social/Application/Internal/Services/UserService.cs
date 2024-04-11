using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Exceptions;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Application.Exceptions;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Queries;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Collectioneer.API.Social.Application.Internal.Services
{
	public class UserService(
		IUnitOfWork unitOfWork, 
		IUserRepository userRepository, 
		IConfiguration configuration, 
		ICollectibleRepository collectibleRepository) : IUserService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IUserRepository _userRepository = userRepository;
		private readonly ICollectibleRepository _collectibleRepository = collectibleRepository;
		private readonly IConfiguration _configuration = configuration;

        public async Task<int> RegisterNewUser(UserRegisterCommand command)
		{
			if (!await _userRepository.IsEmailUnique(command.Email))
			{
				throw new DuplicatedCredentialsException($"Email {command.Email} is already in use.");
			}

			if (!await _userRepository.IsUsernameUnique(command.Username))
			{
				throw new DuplicatedCredentialsException($"Username {command.Username} is already in use.");
			}

			var user = new User(command.Username, command.Email, command.Name, HashPassword(command.Password));

			await _userRepository.Add(user);
			await _unitOfWork.CompleteAsync();

			return user.Id;
		}

        public async Task<string> LoginUser(UserLoginQuery query)
        {
            var user = await _userRepository.GetUserData(query.Username);

            if (user == null || !user.CheckPassword(HashPassword(query.Password)))
            {
				throw new UserNotFoundException($"User with username {query.Username} not found.");
            }

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_KEY"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["JWT_ISSUER"],
                        _configuration["JWT_AUDIENCE"],
                        null,
                        expires: DateTime.Now.AddMinutes(120),
                        signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }

			// TODO: It would be interesting to add a login register to the user, so we can keep track of the last time the user logged in.
        }

		public async Task DeleteUser(UserDeleteCommand command)
		{
			if (!await _userRepository.IsValidUser(command.Username, command.Password))
			{
				throw new UserNotFoundException($"User with username {command.Username} not found.");
			}

			var user = await _userRepository.GetUserData(command.Username);

			if (
				user.Auctions.Count != 0 ||
				user.Bids.Count != 0
				)
			{
				throw new ModelIntegrityException("User has active auctions or bids. Cannot delete user.");
			}

			if(user.Collectibles.Count != 0)
			{
				try
                {
                    await _collectibleRepository.DeleteUserCollectibles(user.Id);
                }
				catch (Exception)
				{
                    throw new ModelIntegrityException("User has collectibles that could not be deleted. Cannot delete user.");
                }
			}

			await _userRepository.Delete(user.Id);
			await _unitOfWork.CompleteAsync();
		}

		public string HashPassword(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            return hash;
        }
    }
}
