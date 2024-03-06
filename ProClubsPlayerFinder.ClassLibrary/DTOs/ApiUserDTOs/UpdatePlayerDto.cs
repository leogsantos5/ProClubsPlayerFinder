using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProClubsPlayerFinder.ClassLibrary.DTOs.ApiUserDTOs
{
    public class UpdatePlayerDto
    {

        [Required]
        public string? Country { get; set; }

        [Required]
        public string? GamingPlatformAccountId { get; set; }

        [Required]
        public string? Console { get; set; }

        public string? Description { get; set; }
    }
}
