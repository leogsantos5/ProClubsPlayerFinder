using Microsoft.AspNetCore.Identity;

namespace ProClubsPlayerFinder.API.Data
{
    public class ApiUser : IdentityUser // This is the old Player class
    {
        public int? ClubId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Description { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Country { get; set; }

        public string? GamingPlatformAccountId { get; set; }

        public string? Console { get; set; }

        public virtual Club? Club { get; set; }
    }
}
