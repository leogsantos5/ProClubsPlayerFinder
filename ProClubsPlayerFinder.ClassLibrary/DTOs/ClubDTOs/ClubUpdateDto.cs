using ProClubsPlayerFinder.ClassLibrary;
using System.ComponentModel.DataAnnotations;

namespace ProClubsPlayerFinder.ClassLibrary.DTOs.ClubDTOs
{
    public class ClubUpdateDto
    {
        [Required]
        public int ClubId { get; set; }

        [Required]
        public string ClubName { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        public string Console { get; set; }

    }
}
