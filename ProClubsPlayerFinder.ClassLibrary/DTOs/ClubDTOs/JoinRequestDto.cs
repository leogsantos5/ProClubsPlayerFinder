using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProClubsPlayerFinder.ClassLibrary.DTOs.ClubDTOs
{
    public class JoinRequestDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string GamingPlatformAccountId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

    }
}
