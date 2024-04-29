using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProClubsPlayerFinder.API.Contracts;
using ProClubsPlayerFinder.API.Data;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ApiUserDTOs;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClassLibraryUserDTOs;

namespace ProClubsPlayerFinder.API.Repositories
{
    public class PlayersRepository : GenericRepository<ApiUser>, IPlayersRepository
    {
        private readonly UserManager<ApiUser> userManager;
        private readonly ClubsPlayerFinderEafc24Context context;
        //private readonly IMapper mapper;
        //private readonly AutoMapper.IConfigurationProvider configurationProvider;

        public PlayersRepository(ClubsPlayerFinderEafc24Context context, UserManager<ApiUser> userManager) : base(context)
        {
            this.userManager = userManager;
            this.context = context;
            //this.mapper = mapper;
            //this.configurationProvider = configurationProvider;
        }

        public List<ApiUserDto> GetFreeAgents()
        {
            var freeAgents = GetAllAsync().Result.Where(p => p.ClubId == null).Take(10);
            var freeAgentsDtos = freeAgents.Select(player => new ApiUserDto {
                                                Email = player.Email,
                                                FirstName = player.FirstName,
                                                LastName = player.LastName,
                                                DateOfBirth = player.DateOfBirth,
                                                Country = player.Country,
                                                GamingPlatformAccountId = player.GamingPlatformAccountId,
                                                Console = player.Console
                                            }).ToList(); // Only gets free agents

            return freeAgentsDtos;
        }

        public async Task<UpdatePlayerDto> GetPlayer(string? idOrEmail)
        {
            ApiUser player = new();
            if (idOrEmail.Contains("@"))
                player = await context.Players.FirstOrDefaultAsync(c => c.Email == idOrEmail);
            else
                player = GetAsync(idOrEmail).Result;
            if (player == null)
                return null; // Return a 404 Not Found if the player is not found

            UpdatePlayerDto apiUserDto = new UpdatePlayerDto
            {
                Country = player.Country,
                Console = player.Console,
                GamingPlatformAccountId = player.GamingPlatformAccountId,
                Description = player.Description!,
            };

            return apiUserDto;
        }

        public async Task<bool> UpdatePlayer(string id, UpdatePlayerDto updatePlayerDto)
        {
            var player = await context.Players.FindAsync(id);
            if (player == null)
                return false; // Or handle the case where the club with the given id is not found

            player.Country = updatePlayerDto.Country;
            player.GamingPlatformAccountId = updatePlayerDto.GamingPlatformAccountId;
            player.Description = updatePlayerDto.Description;
            player.Console = updatePlayerDto.Console;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AcceptClubJoinInvite(int inviteId)
        {
            Invite? invite = await context.Invites.Where(i => i.Id == inviteId).FirstOrDefaultAsync();
            var newPlayerId = invite!.ApiUserId;
            var newPlayer = await GetAsync(newPlayerId);
            var clubId = invite!.ClubId;
            var club = context.Clubs.FindAsync(clubId).Result;
            await userManager.RemoveFromRoleAsync(newPlayer, "Free Agent");
            await userManager.AddToRoleAsync(newPlayer, "Player");
            club!.Players.Add(newPlayer);

            context.Invites.Remove(invite!);
            Request? request = await context.Requests.Where(i => i.ApiUserId == newPlayerId).FirstOrDefaultAsync();
            if (request != null)
                context.Requests.Remove(request);

            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RejectClubJoinInvite(int inviteId)
        {
            Invite? invite = await context.Invites.Where(i => i.Id == inviteId).FirstOrDefaultAsync();
            context.Invites.Remove(invite!);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RequestToJoinClub(int clubId, string playerId)
        {
            Request request = new Request
            {
                ClubId = clubId,
                ApiUserId = playerId
            };
            context.Requests.Add(request);
            await context.SaveChangesAsync();

            return true;            
        }
    }
}
