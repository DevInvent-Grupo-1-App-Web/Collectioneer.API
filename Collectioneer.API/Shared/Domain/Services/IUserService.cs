using Collectioneer.API.Shared.Application.External.Objects;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Queries;

namespace Collectioneer.API.Shared.Domain.Services
{
    public interface IUserService
    {
		/// <summary>
		/// Registers a new user.
		/// Throws and exception if the email and username are not unique.
		/// </summary>
		/// <param name="command"></param>
		/// <returns><see cref="UserDTO"/></returns>
        public Task<UserDTO> RegisterNewUser(UserRegisterCommand command);
		/// <summary>
		/// Logs in a user. 
		/// Throws an exception if the user does not exists.
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
        public Task<string> LoginUser(UserLoginQuery query);
		/// <summary>
		/// Gets a user by username. 
		/// Throws an exception if the user does not exists.
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		public Task<UserDTO> GetUserByUsername(string username);
		/// <summary>
		/// Gets a user by id.
		/// Throws an exception if the user does not exists.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public Task<UserDTO> GetUser(int id);
		/// <summary>
		/// Deletes a user. Throws an exception if the user does not exists.
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
        public Task DeleteUser(UserDeleteCommand query);
		/// <summary>
		/// Hashes a password.
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
        public string HashPassword(string password);
		/// <summary>
		/// Gets a user id by token.
		/// Throws an exception if the token does not contain a claim for the user id.
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
        public Task<int> GetUserIdByToken(string? token);
		public Task ForgotPassword(ForgotPasswordCommand command);
    }
}
