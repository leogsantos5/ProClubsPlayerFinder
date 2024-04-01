using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProClubsPlayerFinder.ClassLibrary.DTOs.ClubDTOs
{
    public class ClubDetailsDto
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

        [Required]
        public int NumberOfPlayers { get; set; }
    }
}
