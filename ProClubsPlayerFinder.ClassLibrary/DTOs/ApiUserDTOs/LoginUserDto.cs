using System.ComponentModel.DataAnnotations;

namespace ProClubsPlayerFinder.ClassLibrary.DTOs.ApiUserDTOs
{
    public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
