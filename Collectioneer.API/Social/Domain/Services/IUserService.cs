using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Entities;
using Collectioneer.API.Social.Domain.Queries;

namespace Collectioneer.API.Social.Domain.Services
{
    public interface IUserService
    {
        public Task<int> RegisterNewUser(UserRegisterCommand command);

        public Task<IEnumerable<User>> GetUsers();

        public Task<string?> LoginUser(UserLoginQuery query);
    }
}
