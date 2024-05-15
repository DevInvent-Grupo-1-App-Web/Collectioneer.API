using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Repositories;
using Collectioneer.API.Shared.Application.Exceptions;
using Collectioneer.API.Shared.Application.External.Objects;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Models.Aggregates;
using Collectioneer.API.Shared.Domain.Queries;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Shared.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Collectioneer.API.Shared.Application.Internal.Services
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
		private readonly CommunicationService _communicationService = new CommunicationService(configuration);

		public async Task<UserDTO> RegisterNewUser(UserRegisterCommand command)
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

			return new UserDTO(user);
		}

		public async Task<string> LoginUser(UserLoginQuery query)
		{
			var user = await _userRepository.GetUserByUsername(query.Username);

			if (user == null || !user.CheckPassword(HashPassword(query.Password)))
			{
				throw new UserNotFoundException($"User with username {query.Username} not found.");
			}

			try
			{
				var jwtKey = _configuration["JWT_KEY"];
				if (jwtKey == null)
				{
					throw new ArgumentNullException(nameof(jwtKey), "JWT_KEY is not set in the configuration.");
				}

				var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
				var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

				var token = new JwtSecurityToken(_configuration["JWT_ISSUER"],
						_configuration["JWT_AUDIENCE"],
						null,
						expires: DateTime.Now.AddDays(30),
						signingCredentials: credentials);

				return new JwtSecurityTokenHandler().WriteToken(token);
			}
			catch (Exception)
			{
				throw;
			}

			// TODO: It would be interesting to add a login register to the user, so we can keep track of the last time the user logged in.
		}

		public async Task<UserDTO> GetUser(int id)
		{
			var user = await _userRepository.GetById(id) ??
				throw new UserNotFoundException($"User with id {id} not found.");

			return new UserDTO(user);
		}

		public async Task DeleteUser(UserDeleteCommand command)
		{
			if (!await _userRepository.IsValidUser(command.Username, command.Password))
			{
				throw new UserNotFoundException($"User with username {command.Username} not found.");
			}

			var user = await _userRepository.GetUserByUsername(command.Username);

			if (
				user?.Auctions.Count != 0 ||
				user.Bids.Count != 0
				)
			{
				throw new ModelIntegrityException("User has active auctions or bids. Cannot delete user.");
			}

			if (user.Collectibles.Count != 0)
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

		public async Task<int> GetUserIdByToken(string? token)
		{
			var handler = new JwtSecurityTokenHandler();
			var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

			var claim = (jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "unique_name")) ??
			throw new ArgumentException("Invalid token. The token does not contain a 'unique_name' claim.");

			var username = claim.Value;

			var user = await GetUserByUsername(username);

			return user.Id;
		}

		public async Task<UserDTO> GetUserByUsername(string username)
		{
			var user = await _userRepository.GetUserByUsername(username) ??
				throw new UserNotFoundException($"User with username {username} not found.");

			return new UserDTO(user);
		}

		public async Task ForgotPassword(ForgotPasswordCommand command)
		{
			_ = await _userRepository.GetUserByEmail(command.Email) ??
				throw new UserNotFoundException($"User with email {command.Email} not found.");

			var subject = "Collectioneer account recovery";
			var body = $"Your recovery token is:\n{GenerateRecoveryToken(command.Email)}.\n\nUse this code to recover your account password. It has a validity of 24 hours. If you did not request this code, please ignore this email.";
			await _communicationService.SendEmail(command.Email, subject, body);
		}

		public async Task ChangeUserPassword(PasswordChangeCommand command)
		{
			if (GenerateRecoveryToken(await GetEmailByUsername(command.Username)) != command.RecoveryToken)
			{
				throw new ArgumentException("Invalid recovery token.");
			}

			// Change the user's password
			var user = await _userRepository.GetUserByUsername(command.Username) ??
				throw new UserNotFoundException($"User with email {command.Username} not found.");
			user.SetPassword(HashPassword(command.NewPassword));

			await _unitOfWork.CompleteAsync();

			// Send an email to the user confirming the password change
			var subject = "Collectioneer password change";
			var body = "Your password has been successfully changed. If you did not request this change, please contact support immediately.";
			await _communicationService.SendEmail(user.Email, subject, body);

		}

		private string GenerateRecoveryToken(string email)
		{
			var jwtKey = _configuration["JWT_KEY"];
			var date = DateTime.UtcNow.Date;
			var input = $"{jwtKey}{date}{email}";
			var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
			var recoveryCode = BitConverter.ToUInt32(hash, 0) % 1000000;

			return recoveryCode.ToString("D6");
		}

		private async Task<string> GetEmailByUsername(string username)
		{
			var user = await _userRepository.GetUserByUsername(username) ??
				throw new UserNotFoundException($"User with username {username} not found.");
				
			return user.Email;
		}
	}
}
