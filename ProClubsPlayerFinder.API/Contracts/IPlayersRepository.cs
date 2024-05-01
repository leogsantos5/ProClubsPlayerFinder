using ProClubsPlayerFinder.API.Data;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ApiUserDTOs;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClassLibraryUserDTOs;

namespace ProClubsPlayerFinder.API.Contracts
{
    public interface IPlayersRepository : IGenericRepository<ApiUser>
    {
        List<ApiUserDto> GetFreeAgents();
        Task<UpdatePlayerDto> GetPlayer(string? idOrEmail);
        Task<bool> LeaveClub(string? idOrEmail);
        Task<bool> UpdatePlayer(string id, UpdatePlayerDto updatePlayerDto);
        Task<bool> AcceptClubJoinInvite(int inviteId);
        Task<bool> RejectClubJoinInvite(int inviteId);
        Task<bool> RequestToJoinClub(int clubId, string playerId);
    }
}
