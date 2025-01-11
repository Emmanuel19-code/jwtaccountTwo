using jwtaccount_two.Domain.Contracts;

namespace jwtaccount_two.Service
{
    public interface IUserService
    {
        Task<UserResponse>RegisterAsync(UserRegisterRequest request);
        Task<CurrentUserResponse>GetCurrentUserAsync();
        Task<UserResponse>GetByIdAsync(Guid Id);
        Task<UserResponse>UpdateAsync(Guid Id,UpdateUserRequest request);
        Task DeleteAsync(Guid Id);
        Task RevokeRefreshToken();
        Task<CurrentUserResponse> RefreshTokenAsync(RemoveTokenRequest request);
        Task<UserResponse>LoginAsync(UserLoginRequest request);
    }
}