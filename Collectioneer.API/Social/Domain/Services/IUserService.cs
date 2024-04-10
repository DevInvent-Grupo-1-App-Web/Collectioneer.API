using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Aggregates;
using Collectioneer.API.Social.Domain.Queries;

namespace Collectioneer.API.Social.Domain.Services
{
    public interface IUserService
    {
        public Task<int> RegisterNewUser(UserRegisterCommand command);
        public Task<string> LoginUser(UserLoginQuery query);
		public Task DeleteUser(UserDeleteCommand query);
        public string HashPassword(string password);
    }
}
