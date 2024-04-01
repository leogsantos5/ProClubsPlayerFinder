using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProClubsPlayerFinder.API.Data
{
    public class Request
    {
        public int Id { get; set; }

        [Required]
        public string ApiUserId { get; set; }

        [Required]
        public int ClubId { get; set; }

        //[MaxLength(256)]
        //public string Message { get; set; }

        //[Required]
        //[MaxLength(20)]
        //public string Status { get; set; } = "Pending";

        public virtual ApiUser ApiUser { get; set; }
        public virtual Club Club { get; set; }
    }
}
