using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using jwtaccount_two.Domain.Contracts;
using jwtaccount_two.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace jwtaccount_two.Service
{
    public class TokenServiceImple : ITokenService
    {
        private readonly SymmetricSecurityKey _secretKey;
        private readonly string? _validIssuer;
        private readonly string? _validAudience;
        private readonly double _expires;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TokenServiceImple> _logger;
        public TokenServiceImple(IConfiguration configuration,UserManager<ApplicationUser> userManager,ILogger<TokenServiceImple>logger)
        {
            _userManager = userManager;
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            if(jwtSettings is null || string.IsNullOrEmpty(jwtSettings.Key))
            {
                throw new InvalidOperationException("Jwt Secret key is not configured");
            }
            _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            _validIssuer = jwtSettings.ValidIssuer;
            _validAudience = jwtSettings.ValidAudience;
            _expires = jwtSettings.Expire;
            _logger = logger;
            _userManager = userManager;
        }
        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user?.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("FirstName",user.FirstName),
                new Claim("LastName",user.LastName),
                new Claim("Gender",user.Gender)

            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles){
                claims.Add(new Claim(ClaimTypes.Role,role));
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,List<Claim>claims)
        {
            return new JwtSecurityToken(
                issuer:_validIssuer,
                audience: _validAudience,
                claims : claims,
                expires:DateTime.Now.AddMinutes(_expires),
                signingCredentials : signingCredentials
            );
        }
        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var signingCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);
            var claims =await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials,claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public string GenerateRefreshToken()
        {
           var randomNumber = new byte[64];
           using var rng = RandomNumberGenerator.Create();
           rng.GetBytes(randomNumber);
           var refreshToken = Convert.ToBase64String(randomNumber);
           return refreshToken;
        }
    }
}