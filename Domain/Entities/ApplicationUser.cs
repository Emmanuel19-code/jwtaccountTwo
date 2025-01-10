using Microsoft.AspNetCore.Identity;

namespace jwtaccount_two.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenTimeExpiery { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

 