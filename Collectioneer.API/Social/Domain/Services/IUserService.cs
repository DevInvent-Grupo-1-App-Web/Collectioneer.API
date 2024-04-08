using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Entities;

namespace Collectioneer.API.Social.Domain.Services
{
    public interface IUserService
    {
        public Task<int> RegisterNewUser(UserRegisterCommand command);

        public Task<IEnumerable<User>> GetUsers();
    }
}
