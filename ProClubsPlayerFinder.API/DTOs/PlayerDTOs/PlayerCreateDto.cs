using System.ComponentModel.DataAnnotations;

namespace ProClubsPlayerFinder.API.DTOs.PlayerDTOs
{
    public class PlayerCreateDto
    {
        [Required] 
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string GamingPlatformAccountId { get; set; }
        [Required]
        public string Console { get; set; }
    }
}
