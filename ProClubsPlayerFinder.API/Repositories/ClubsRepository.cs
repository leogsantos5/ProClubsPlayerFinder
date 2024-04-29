using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProClubsPlayerFinder.API.Contracts;
using ProClubsPlayerFinder.API.Data;
using ProClubsPlayerFinder.ClassLibrary;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClassLibraryUserDTOs;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClubDTOs;

namespace ProClubsPlayerFinder.API.Repositories
{
    public class ClubsRepository : GenericRepository<Club>, IClubsRepository
    {
        private readonly UserManager<ApiUser> userManager;
        private readonly ClubsPlayerFinderEafc24Context context;
        //private readonly IMapper mapper;
        //private readonly AutoMapper.IConfigurationProvider configurationProvider;

        public ClubsRepository(ClubsPlayerFinderEafc24Context context, UserManager<ApiUser> userManager) : base(context)
        {
            this.userManager = userManager;
            this.context = context;
            //this.mapper = mapper;
            //this.configurationProvider = configurationProvider;
        }

        public List<ClubUpdateDto> GetClubs()
        {
            var clubs = GetAllAsync().Result.Take(10);
            var clubUpdateDtos = clubs.Select(club => new ClubUpdateDto { ClubId = club.Id, ClubName = club.ClubName, Console = club.Console, Description = club.Description });
            return clubUpdateDtos.ToList();
        }

        public ClubUpdateDto GetClub(int? clubId)
        {
            var club = GetAsync(clubId).Result;
            if (club == null)
                return null; // Return a 404 Not Found if the club is not found

            ClubUpdateDto clubUpdateDto = new ClubUpdateDto
            {
                ClubId = club.Id!,
                ClubName = club.ClubName!,
                Description = club.Description!,
                Console = club.Console!
            };

            return clubUpdateDto;
        }

        public ClubDetailsDto GetClubDetails(int? id)
        {
            var club = GetAsync(id).Result;

            if (club == null)
                return null;

            var clubPlayersCount = context.Players.Where(p => p.ClubId == id).Count();

            ClubDetailsDto clubUpdateDto = new ClubDetailsDto
            {
                ClubId = club.Id!,
                ClubName = club.ClubName!,
                Description = club.Description!,
                Console = club.Console!,
                NumberOfPlayers = clubPlayersCount
            };

            return clubUpdateDto;
        }

        public async Task<bool> UpdateClub(int id, ClubUpdateDto clubUpdateDto)
        {
            var club = GetAsync(id).Result;
            if (club == null)
                return false; // Or handle the case where the club with the given id is not found

            club.ClubName = clubUpdateDto.ClubName;
            club.Description = clubUpdateDto.Description;
            club.Console = clubUpdateDto.Console;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetClubIdFromPlayerOwnerId(string playerOwnerId)
        {
            var playerOwner = await context.Players.FindAsync(playerOwnerId);
            return GetAsync(playerOwner!.ClubId).Result!.Id;
        }
        public async Task<Club> CreateClub(ClubCreateDto clubToCreate, string authenticatedUserId)
        {
            ApiUser? playerOwner = await userManager.FindByIdAsync(authenticatedUserId);
            if (playerOwner == null)
                return null;

            var club = new Club
            {
                ClubName = clubToCreate.ClubName,
                Description = clubToCreate.Description,
                OwnerPlayerId = playerOwner.Id,
                Console = playerOwner.Console,
                Players = new List<ApiUser>() // Initialize with an empty list of players
            };

            club.OwnerPlayer = playerOwner;
            await userManager.RemoveFromRoleAsync(playerOwner, "Free Agent");
            await userManager.AddToRoleAsync(playerOwner, "Club Owner");
            await AddAsync(club);

            var newClub = await context.Clubs.FirstOrDefaultAsync(c => c.OwnerPlayerId == playerOwner.Id);
            if (newClub == null)
                return null;
            playerOwner.ClubId = newClub.Id;
            await context.SaveChangesAsync();

            return club;
        }

        public async Task<bool> DeleteClub(string playerOwnerId)
        {
            var playerOwner = await context.Players.FindAsync(playerOwnerId);
            if (playerOwner == null)
                return false;
            List<ApiUser> playersInClub = await context.Players.Where(p => p.ClubId == playerOwner.ClubId).ToListAsync();

            var club = await GetAsync(playerOwner.ClubId);
            if (club == null)
                return false;
            context.Clubs.Remove(club);

            foreach (var player in playersInClub)
            {
                player.ClubId = null;
                if (playerOwnerId == player.Id)
                    await userManager.RemoveFromRoleAsync(player, "Club Owner");
                else
                    await userManager.RemoveFromRoleAsync(player, "Player");
                await userManager.AddToRoleAsync(player, "Free Agent");
            }

            await context.SaveChangesAsync();
            return true;
        }

        public List<ApiUser> GetClubPlayers(int? id)
        {
            return GetAsync(id).Result.Players.ToList();
        }

        public async Task<bool> RemovePlayer(string clubOwnerId, string playerEmail)
        {
            var clubOwner = await context.Players.FindAsync(clubOwnerId);
            if (clubOwner == null || clubOwner.ClubId == null)
                return false;

            var playerToRemove = await context.Players.FirstOrDefaultAsync(p => p.Email == playerEmail && p.ClubId == clubOwner.ClubId);
            if (playerToRemove == null)
                return false;

            playerToRemove.ClubId = null;
            await userManager.RemoveFromRoleAsync(playerToRemove, "Player");
            await userManager.AddToRoleAsync(playerToRemove, "Free Agent");
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ApiUserDto>> GetClubPlayers(string clubOwnerId, bool includeOwner)
        {
            Club? club = await context.Clubs.Where(c => c.OwnerPlayerId == clubOwnerId).FirstOrDefaultAsync();

            if (club == null)
                return null; // Club not found for the given owner ID

            var clubPlayers = await context.Players.Where(p => p.ClubId == club.Id).OrderBy(p => p.GamingPlatformAccountId).ToListAsync();

            if (!includeOwner)
                clubPlayers = clubPlayers.Where(p => p.Id != clubOwnerId).ToList();

            var clubPlayersDto = clubPlayers.Select(player => new ApiUserDto
            {
                Email = player.Email,
                FirstName = player.FirstName,
                LastName = player.LastName,
                DateOfBirth = player.DateOfBirth,
                Country = player.Country,
                GamingPlatformAccountId = player.GamingPlatformAccountId,
                Console = player.Console
            }).Take(10).ToList();

            return clubPlayersDto;
        }

        public async Task<List<JoinRequestDto>> GetClubJoinRequests(string clubOwnerId)
        {
            Club? club = await context.Clubs.Where(c => c.OwnerPlayerId == clubOwnerId).FirstOrDefaultAsync();

            List<Request> requests = await context.Requests
                .Where(r => r.ClubId == club.Id)
                .ToListAsync();

            List<JoinRequestDto> requestDtos = requests.Select(r => new JoinRequestDto
            {
                Id = r.Id,
                GamingPlatformAccountId = GetGamingPlatformAccountIdFromApiUserId(r.ApiUserId)
            }).ToList();

            return requestDtos;
        }

        public async Task<bool> InvitePlayerToJoinClub(string clubOwnerId, string gamingPlatformAccountId)
        {
            var playerId = context.Players.Where(p => p.GamingPlatformAccountId == gamingPlatformAccountId).FirstOrDefaultAsync().Result!.Id;
            var clubId = context.Clubs.Where(c => c.OwnerPlayerId == clubOwnerId).FirstOrDefaultAsync().Result!.Id;
            Invite invite = new Invite
            {
                ClubId = clubId,
                ApiUserId = playerId
            };
            context.Invites.Add(invite);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ClubInviteDto>> GetClubJoinInvites(string playerId)
        {
            List<Invite> invites = await context.Invites
                    .Where(i => i.ApiUserId == playerId)
                    .ToListAsync();

            List<ClubInviteDto> invitesDtos = invites.Select(i => new ClubInviteDto
            {
                Id = i.Id,
                ClubName = GetClubNameFromClubId(i.ClubId)
            }).ToList();

            return invitesDtos;           
        }

        public async Task<bool> AcceptPlayerJoinRequest(int requestId)
        {
            Request? request = await context.Requests.Where(i => i.Id == requestId).FirstOrDefaultAsync();
            var newPlayerId = request!.ApiUserId;
            var newPlayer = await context.Players.FindAsync(newPlayerId);
            var clubId = request!.ClubId;
            var club = GetAsync(clubId).Result;
            await userManager.RemoveFromRoleAsync(newPlayer, "Free Agent");
            await userManager.AddToRoleAsync(newPlayer, "Player");
            club!.Players.Add(newPlayer);

            context.Requests.Remove(request!);
            Invite? invite = await context.Invites.Where(i => i.ApiUserId == newPlayerId).FirstOrDefaultAsync();
            if (invite != null)
                context.Invites.Remove(invite);

            await context.SaveChangesAsync();
            return true;         
        }

        public async Task<bool> RejectPlayerJoinRequest(int requestId)
        {
            Request? request = await context.Requests.Where(i => i.Id == requestId).FirstOrDefaultAsync();
            context.Requests.Remove(request!);
            await context.SaveChangesAsync();
            return true;
        }

        private string GetGamingPlatformAccountIdFromApiUserId(string playerId)
        {
            return context.Users.FirstOrDefaultAsync(p => p.Id == playerId).Result!.GamingPlatformAccountId!;
        }

        private string GetClubNameFromClubId(int clubId)
        {
            return context.Clubs.FirstOrDefaultAsync(c => c.Id == clubId).Result!.ClubName!;
        }
    }
}
