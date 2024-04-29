using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProClubsPlayerFinder.API.Data;
using ProClubsPlayerFinder.API.Repositories;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClassLibraryUserDTOs;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClubDTOs;

namespace ProClubsPlayerFinder.API.Contracts
{
    public interface IClubsRepository : IGenericRepository<Club>
    {
        List<ClubUpdateDto> GetClubs();
        ClubUpdateDto GetClub(int? clubId);
        ClubDetailsDto GetClubDetails(int? id);
        Task<bool> UpdateClub(int id, ClubUpdateDto clubUpdateDto);
        Task<int> GetClubIdFromPlayerOwnerId(string playerOwnerId);
        Task<Club> CreateClub(ClubCreateDto clubToCreate, string authenticatedUserId);
        Task<bool> DeleteClub(string playerOwnerId);
        List<ApiUser> GetClubPlayers(int? id);
        Task<bool> RemovePlayer(string playerEmail, string clubOwnerId);
        Task<List<ApiUserDto>> GetClubPlayers(string clubOwnerId, bool includeOwner);
        Task<List<JoinRequestDto>> GetClubJoinRequests(string clubOwnerId);
        Task<bool> InvitePlayerToJoinClub(string clubOwnerId, string gamingPlatformAccountId);
        Task<List<ClubInviteDto>> GetClubJoinInvites(string playerId);
        Task<bool> AcceptPlayerJoinRequest(int requestId);
        Task<bool> RejectPlayerJoinRequest(int requestId);
    }
}
