﻿using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Domain.Models.Entities;

namespace Collectioneer.API.Social.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<bool> IsEmailUnique(string email);

        public Task<bool> IsUsernameUnique(string username);
    }
}
