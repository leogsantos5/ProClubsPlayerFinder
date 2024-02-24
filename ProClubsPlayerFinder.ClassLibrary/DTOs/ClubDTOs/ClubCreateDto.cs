using System.ComponentModel.DataAnnotations;

namespace ProClubsPlayerFinder.ClassLibrary.DTOs.ClubDTOs
{
    public class ClubCreateDto
    {
        //public string OwnerPlayerId { get; set; }
        [Required]
        [StringLength(256)]
        public string Description { get; set; }
        [Required]
        public string ClubName { get; set; }
    }
}
