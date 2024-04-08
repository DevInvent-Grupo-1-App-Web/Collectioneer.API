using AutoMapper;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Application.Exceptions;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Entities;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;

namespace Collectioneer.API.Social.Application.Internal.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

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

            var user = _mapper.Map<User>(command);

            await _userRepository.Add(user);
            await _unitOfWork.CompleteAsync();

            return user.Id;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetAll();
        }
    }
}
