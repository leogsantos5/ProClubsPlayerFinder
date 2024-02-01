using Microsoft.AspNetCore.Identity;

namespace ProClubsPlayerFinder.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? GamingPlatformAccountId { get; set; } // can be PS5, Xbox X or PC

        public string? Country { get; set; }

    }
}
