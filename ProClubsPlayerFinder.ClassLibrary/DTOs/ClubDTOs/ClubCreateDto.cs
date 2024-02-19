using System.ComponentModel.DataAnnotations;

namespace ProClubsPlayerFinder.ClassLibrary.DTOs.ClubDTOs
{
    public class ClubCreateDto
    {
        [Required]
        public int OwnerPlayerId { get; set; }
        [Required]
        [StringLength(256)]
        public string Description { get; set; }
        [Required]
        public string ClubName { get; set; }
        [Required]
        public string Console { get; set; }
    }
}
