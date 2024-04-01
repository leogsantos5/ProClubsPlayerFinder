using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProClubsPlayerFinder.API.Data;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ApiUserDTOs;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClassLibraryUserDTOs;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClubDTOs;
using static System.Reflection.Metadata.BlobBuilder;

namespace ProClubsPlayerFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClubsController : ControllerBase
    {
        private readonly ClubsPlayerFinderEafc24Context context;
        private readonly UserManager<ApiUser> userManager;

        public ClubsController(UserManager<ApiUser> _userManager, ClubsPlayerFinderEafc24Context _context)
        {
            context = _context;
            userManager = _userManager;
        }

        // GET: All Clubs
        [HttpGet("GetClubs")]
        public async Task<ActionResult<IEnumerable<ClubUpdateDto>>> GetClubs()
        {
            var clubDtos = await context.Clubs
                .Select(club => new ClubUpdateDto
                {
                    ClubId = club.Id,
                    ClubName = club.ClubName,
                    Console = club.Console,
                    Description = club.Description
                }).Take(10).ToListAsync();

            return Ok(clubDtos);
        }

        // GET: Gets specific Club
        [HttpGet("GetClub/{id}")]
        public async Task<ActionResult<Club>> GetClub(int? id)
        {
            var club = await context.Clubs.FirstOrDefaultAsync(c => c.Id == id);
            if (club == null)
                return NotFound(); // Return a 404 Not Found if the club is not found

            ClubUpdateDto clubUpdateDto = new ClubUpdateDto
            {
                ClubId = club.Id!,
                ClubName = club.ClubName!,
                Description = club.Description!,
                Console = club.Console!
            };

            return Ok(clubUpdateDto);
        }

        // GET: Gets specific Club details
        [HttpGet("GetClubDetails/{id}")]
        public async Task<ActionResult<Club>> GetClubDetails(int? id)
        {
            var club = await context.Clubs.FirstOrDefaultAsync(c => c.Id == id);
            if (club == null)
                return NotFound(); // Return a 404 Not Found if the club is not found

            var clubPlayersCount = context.Players.Where(p => p.ClubId == id).Count();

            ClubDetailsDto clubUpdateDto = new ClubDetailsDto
            {
                ClubId = club.Id!,
                ClubName = club.ClubName!,
                Description = club.Description!,
                Console = club.Console!,
                NumberOfPlayers = clubPlayersCount
            };

            return Ok(clubUpdateDto);
        }

        [Authorize(Roles = "Club Owner")]
        [HttpPut("UpdateClub/{id}")]
        public async Task<IActionResult> UpdateClub(int id, ClubUpdateDto clubUpdateDto)
        {
            try
            {
                var club = await context.Clubs.FindAsync(id);
                if (club == null)
                    return NotFound(); // Or handle the case where the club with the given id is not found

                club.ClubName = clubUpdateDto.ClubName;
                club.Description = clubUpdateDto.Description;
                club.Console = clubUpdateDto.Console;

                await context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        // GET: Gets specific Club
        [HttpGet("GetClubId/{playerOwnerId}")]
        [Authorize(Roles = "Club Owner")]
        public async Task<ActionResult<int>> GetClubIdFromPlayerOwnerId(string playerOwnerId)
        {
            try
            {
                var playerOwner = await context.Players.FindAsync(playerOwnerId);
                int clubId = context.Clubs.FindAsync(playerOwner!.ClubId).Result!.Id;
                return Ok(clubId);
            }
            catch
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Free Agent")]
        // POST: Creates a Club
        [HttpPost("CreateClub")]
        public async Task<ActionResult<Club>> CreateClub([Bind("Description,ClubName")] ClubCreateDto clubToCreate)
        {
            try
            {
                string authenticatedUserId = User.Claims.FirstOrDefault(claim => claim.Type == "uid")!.Value;
                ApiUser? playerOwner = await userManager.FindByIdAsync(authenticatedUserId);
                if (playerOwner == null)
                    return BadRequest(new { Message = "User not found" });

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
                context.Clubs.Add(club);
                await context.SaveChangesAsync();

                var newClub = await context.Clubs.FirstOrDefaultAsync(c => c.OwnerPlayerId == playerOwner.Id);
                if (newClub == null)
                    return StatusCode(500, new { Message = "Error retrieving the created club" });
                playerOwner.ClubId = newClub.Id;
                await context.SaveChangesAsync();

                return Ok(club);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }          
        }

        // DELETE: Deletes a Club
        [HttpDelete("DeleteClub/{playerOwnerId}")]
        [Authorize(Roles = "Club Owner")]
        public async Task<IActionResult> DeleteClub(string playerOwnerId)
        {
            try
            {
                var playerOwner = await context.Players.FindAsync(playerOwnerId);
                if (playerOwner == null)
                    return NotFound(new { Message = "Player not found" });
                List<ApiUser> playersInClub = await context.Players.Where(p => p.ClubId == playerOwner.ClubId).ToListAsync();

                var club = await context.Clubs.FindAsync(playerOwner.ClubId);
                if (club == null)
                    return NotFound(new { Message = "Club not found" });
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
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        // GET: Gets players of a Club
        [HttpGet("GetClubPlayers/{id}")]
        public async Task<IActionResult> GetClubPlayers(int? id)
        {
            var clubPlayers = await context.Clubs.Where(club => club.Id == id).SelectMany(club => club.Players).ToListAsync();

            if (clubPlayers == null || clubPlayers.Count == 0) // it is never not null, it always has at least one, the owner
                return NotFound();

            return Ok(clubPlayers);
        }

        // DELETE: Remove a Player from a Club
        [HttpDelete("RemovePlayer/{clubOwnerId}/{playerEmail}")]
        [Authorize(Roles = "Club Owner")]
        public async Task<IActionResult> RemovePlayer(string clubOwnerId, string playerEmail)
        {
            try
            {
                var clubOwner = await context.Players.FindAsync(clubOwnerId);
                if (clubOwner == null || clubOwner.ClubId == null)
                    return NotFound("Club owner not found or not associated with a club.");

                var playerToRemove = await context.Players.FirstOrDefaultAsync(p => p.Email == playerEmail && p.ClubId == clubOwner.ClubId);
                if (playerToRemove == null)
                    return NotFound("Player not found.");

                playerToRemove.ClubId = null;
                await userManager.RemoveFromRoleAsync(playerToRemove, "Player");
                await userManager.AddToRoleAsync(playerToRemove, "Free Agent");
                await context.SaveChangesAsync();

                return Ok("Player removed from the club successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        [HttpGet("GetClubPlayers/{clubOwnerId}/{includeOwner}")]
        [Authorize(Roles = "Club Owner")]
        public async Task<ActionResult<IEnumerable<ApiUserDto>>> GetClubPlayers(string clubOwnerId, bool includeOwner)
        {
            try
            {
                Club? club = await context.Clubs.Where(c => c.OwnerPlayerId == clubOwnerId).FirstOrDefaultAsync();

                if (club == null)
                    return NotFound(); // Club not found for the given owner ID

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

                return Ok(clubPlayersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        [HttpGet("GetClubJoinRequests/{clubOwnerId}")]
        [Authorize(Roles = "Club Owner")]
        public async Task<ActionResult<IEnumerable<JoinRequestDto>>> GetClubJoinRequests(string clubOwnerId)
        {
            try
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

                return Ok(requestDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        // Get: Requests player to join yout club
        [HttpGet("InvitePlayerToJoinClub/{clubOwnerId}/{gamingPlatformAccountId}")]
        [Authorize(Roles = "Club Owner")]
        public async Task<IActionResult> InvitePlayerToJoinClub(string clubOwnerId, string gamingPlatformAccountId)
        {
            try
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

                return Ok("Invite sucessfully sent.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        [HttpGet("GetClubJoinInvites/{playerId}")]
        [Authorize(Roles = "Free Agent")]
        public async Task<ActionResult<IEnumerable<ClubInviteDto>>> GetClubJoinInvites(string playerId)
        {
            try
            {
                List<Invite> invites = await context.Invites
                    .Where(i => i.ApiUserId == playerId)
                    .ToListAsync();

                List<ClubInviteDto> invitesDtos = invites.Select(i => new ClubInviteDto
                {
                    Id = i.Id,
                    ClubName = GetClubNameFromClubId(i.ClubId)
                }).ToList();

                return Ok(invitesDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        
        [HttpGet("AcceptPlayerJoinRequest/{requestId}")]
        [Authorize(Roles = "Club Owner")]
        public async Task<IActionResult> AcceptPlayerJoinRequest(int requestId)
        {
            try
            {
                Request? request = await context.Requests.Where(i => i.Id == requestId).FirstOrDefaultAsync();
                var newPlayerId = request!.ApiUserId;
                var newPlayer = await context.Players.FindAsync(newPlayerId);
                var clubId = request!.ClubId;
                var club = context.Clubs.FindAsync(clubId).Result;
                await userManager.RemoveFromRoleAsync(newPlayer, "Free Agent");
                await userManager.AddToRoleAsync(newPlayer, "Player");
                club!.Players.Add(newPlayer);

                context.Requests.Remove(request!);
                Invite? invite = await context.Invites.Where(i => i.ApiUserId == newPlayerId).FirstOrDefaultAsync();
                if (invite != null)
                    context.Invites.Remove(invite);

                await context.SaveChangesAsync();

                return Ok("Request sucessfully accepted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        [HttpGet("RejectPlayerJoinRequest/{requestId}")]
        [Authorize(Roles = "Club Owner")]
        public async Task<IActionResult> RejectPlayerJoinRequest(int requestId)
        {
            try
            {
                Request? request = await context.Requests.Where(i => i.Id == requestId).FirstOrDefaultAsync();

                context.Requests.Remove(request!);
                await context.SaveChangesAsync();

                return Ok("Request sucessfully rejected.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
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
