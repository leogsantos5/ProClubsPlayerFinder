using ProClubsPlayerFinder.API.Data;

namespace ProClubsPlayerFinder.API.DTOs.ClubDTOs
{
    public class ClubUpdateDto
    {
        public string? Description { get; set; }
        public virtual ICollection<ApiUser> Players { get; set; } = new List<ApiUser>();
    }
}
