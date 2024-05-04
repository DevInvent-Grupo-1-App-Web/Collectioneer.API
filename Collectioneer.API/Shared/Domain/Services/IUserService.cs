using Collectioneer.API.Shared.Application.External.Objects;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Queries;

namespace Collectioneer.API.Shared.Domain.Services
{
    public interface IUserService
    {
        public Task<UserDTO> RegisterNewUser(UserRegisterCommand command);
        public Task<string> LoginUser(UserLoginQuery query);
		public Task<UserDTO> GetUserByUsername(string username);
        public Task<UserDTO> GetUser(int id);
        public Task DeleteUser(UserDeleteCommand query);
        public string HashPassword(string password);
        public Task<int> GetUserIdByToken(string? token);
    }
}
