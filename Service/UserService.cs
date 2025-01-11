using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using jwtaccount_two.Domain.Contracts;
using jwtaccount_two.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace jwtaccount_two.Service
{
    public class UserService : IUserService
    {
        private readonly TokenServiceImple _tokenService;
        private readonly ICurrentUserService _icurrentUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private IMapper _mapper;
        private ILogger<UserService> _logger;

        public UserService(TokenServiceImple tokenServiceImple,ILogger<UserService> logger,IMapper mapper,ICurrentUserService icurrentUserService){
            _tokenService = tokenServiceImple;
            _logger = logger;
            _mapper = mapper;
            _icurrentUserService  = icurrentUserService;
        }
        public async Task<UserResponse> RegisterAsync(UserRegisterRequest request)
        {
            _logger.LogInformation("User Registering");
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if(existingUser != null)
            {
                throw new Exception("Email Already Available");
            }
            var newUser = _mapper.Map<ApplicationUser>(request);
            newUser.UserName = GenerateUserName(request.FirstName,request.LastName);
            var result = await _userManager.CreateAsync(newUser,request.Password);
            if(!result.Succeeded)
            {
                var errors = string.Join(",",result.Errors.Select(e=>e.Description));
                _logger.LogError($"could not create user, {errors}");
                throw new Exception($"Could not create user {errors}");
            }
            _logger.LogInformation("User create");
            await _tokenService.GenerateToken(newUser);

           return _mapper.Map<UserResponse>(newUser);
        }
        private string GenerateUserName(string FirstName,string LastName)
        {
            var baseUsername = $"{FirstName[0]}{LastName}".ToLower();
            var username = baseUsername;
            var count = 1;
            while(_userManager.Users.Any(u=>u.UserName == username)){
                username = $"{baseUsername}{count++}";
            }
            return baseUsername;
        }
        public Task DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse> GetByIdAsync(Guid Id)
        {
            _logger.LogInformation("Getting user by Id");
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if(user is null)
            {
                _logger.LogError("Could not find user");
                throw new Exception("Could not find user");
            }
            _logger.LogInformation("User Found");
            return _mapper.Map<UserResponse>(user);
        }

        public Task<CurrentUserResponse> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse> LoginAsync(UserLoginRequest request)
        {
            if(request is null)
            {
                _logger.LogError("provided null");
                throw new ArgumentNullException(nameof(request));
            }
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null || !await _userManager.CheckPasswordAsync(user,request.Password))
            {
                _logger.LogError("Invalid Email or Password");
                throw new Exception("Invalid Email or Passoword");
            }
            //generating accessToken
            var token = await _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            
            using var sha256 = SHA256.Create();
            var refreshTokenHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
            user.RefreshToken = Convert.ToBase64String(refreshTokenHash);
            user.RefreshTokenTimeExpiery = DateTime.Now.AddDays(7);

            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(e => e.Description));
                _logger.LogError($"could not create user, {errors}");
                throw new Exception($"Could not create user {errors}");
            }
            var userReponse = _mapper.Map<ApplicationUser,UserResponse>(user);
            userReponse.AccessToken = token;
            userReponse.RefreshToken = refreshToken;
            return userReponse;
        }

        public Task<CurrentUserResponse> RefreshTokenAsync(RemoveTokenRequest request)
        {
            throw new NotImplementedException();
        }

        

        public Task RevokeRefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> UpdateAsync(Guid Id, UpdateUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}