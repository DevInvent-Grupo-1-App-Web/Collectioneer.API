using AutoMapper;
using Collectioneer.API.Shared.Domain.Repositories;
using Collectioneer.API.Social.Application.Exceptions;
using Collectioneer.API.Social.Domain.Commands;
using Collectioneer.API.Social.Domain.Models.Entities;
using Collectioneer.API.Social.Domain.Queries;
using Collectioneer.API.Social.Domain.Repositories;
using Collectioneer.API.Social.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Collectioneer.API.Social.Application.Internal.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
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

        public async Task<string?> LoginUser(UserLoginQuery query)
        {
            if (!await _userRepository.IsValidUser(query.Username, query.Password)) return null;

            var user = await _userRepository.GetUserData(query.Username);

            if (user == null) return null;


            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    null,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
