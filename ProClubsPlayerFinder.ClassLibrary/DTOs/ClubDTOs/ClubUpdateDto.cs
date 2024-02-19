using ProClubsPlayerFinder.ClassLibrary;

namespace ProClubsPlayerFinder.ClassLibrary.DTOs.ClubDTOs
{
    public class ClubUpdateDto
    {
        public string? Description { get; set; }
        public virtual List<int> PlayersIds { get; set; } = new List<int>();
    }
}
