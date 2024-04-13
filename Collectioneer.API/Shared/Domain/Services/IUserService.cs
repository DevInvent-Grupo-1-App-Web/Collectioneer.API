using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Shared.Domain.Commands;
using Collectioneer.API.Shared.Domain.Queries;

namespace Collectioneer.API.Shared.Domain.Services
{
    public interface IUserService
    {
        public Task<int> RegisterNewUser(UserRegisterCommand command);
        public Task<string> LoginUser(UserLoginQuery query);
        public Task DeleteUser(UserDeleteCommand query);
        public string HashPassword(string password);
    }
}
