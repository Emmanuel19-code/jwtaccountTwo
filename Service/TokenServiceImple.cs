using System.Text;
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
            _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:key"]));
            _validIssuer = configuration["JwtSettings:validIssuer"];
            _validAudience = configuration["JwtSettings:validAudience"];
            _expires = Convert.ToDouble(configuration["JwtSettings:expires"]);
            _logger = logger;
            _userManager = userManager;
        }
        public Task<string> GenerateToken(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public string GenerateToken()
        {
            throw new NotImplementedException();
        }
    }
}