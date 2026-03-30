using Microsoft.AspNetCore.Identity;

namespace SOCIALIZE.Models
{
    public class AppUser:IdentityUser
    {
        public string? bio { set; get; }

        public string? profilePicURL { set; get; }

        public string Role { set; get; }

    }
}
