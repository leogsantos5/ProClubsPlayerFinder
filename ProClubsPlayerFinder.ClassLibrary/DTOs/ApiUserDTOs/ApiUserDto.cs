using ProClubsPlayerFinder.ClassLibrary.DTOs.ApiUserDTOs;
using System.ComponentModel.DataAnnotations;

namespace ProClubsPlayerFinder.ClassLibrary.DTOs.ClassLibraryUserDTOs
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

        public string? Description { get; set; }

        public int? ClubId { get; set; }

        public string? Role { get; set; }

    }
}
