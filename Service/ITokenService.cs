namespace jwtaccount_two.Service
{
    public interface ITokenService
    {
        Task<string> GenerateToken(Domain.Entities.ApplicationUser user);
        string GenerateRefreshToken();
    }
}