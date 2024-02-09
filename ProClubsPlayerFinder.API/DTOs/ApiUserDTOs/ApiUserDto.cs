using System.ComponentModel.DataAnnotations;

namespace ProClubsPlayerFinder.API.DTOs.ApiUserDTOs
{
    public class ApiUserDto : LoginUserDto
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string? Country { get; set; }

        [Required]
        public string? GamingPlatformAccountId { get; set; }

        [Required]
        public string? Console { get; set; }

        public string? Role { get; set; }
    }
}
