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
using ProClubsPlayerFinder.API.Contracts;
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
        private readonly IClubsRepository clubsRepository;

        public ClubsController(UserManager<ApiUser> _userManager, ClubsPlayerFinderEafc24Context _context, IClubsRepository clubsRepository)
        {
            context = _context;
            userManager = _userManager;
            this.clubsRepository = clubsRepository;
        }

        // GET: All Clubs
        [HttpGet("GetClubs")]
        public IEnumerable<ClubUpdateDto> GetClubs()
        {
            return clubsRepository.GetClubs();
        }

        // GET: Gets specific Club
        [HttpGet("GetClub/{id}")]
        public ClubUpdateDto GetClub(int? id)
        {
            return clubsRepository.GetClub(id);
        }

        // GET: Gets specific Club details
        [HttpGet("GetClubDetails/{id}")]
        public ClubDetailsDto GetClubDetails(int? id)
        {
            return clubsRepository.GetClubDetails(id);
        }

        [Authorize(Roles = "Club Owner")]
        [HttpPut("UpdateClub/{id}")]
        public async Task<IActionResult> UpdateClub(int id, ClubUpdateDto clubUpdateDto)
        {
            try
            {
                var clubUpdatedSuccessfully = await clubsRepository.UpdateClub(id, clubUpdateDto);
                if (clubUpdatedSuccessfully)
                    return Ok();
                else
                    return StatusCode(500, new { Message = "Internal Server Error" });
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
                var clubId = await clubsRepository.GetClubIdFromPlayerOwnerId(playerOwnerId);
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
                var club = await clubsRepository.CreateClub(clubToCreate, authenticatedUserId);
                return Ok(club);
            }
            catch 
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
                await clubsRepository.DeleteClub(playerOwnerId);
                return NoContent();
            }
            catch 
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        // GET: Gets players of a Club
        [HttpGet("GetClubPlayers/{id}")]
        public async Task<IActionResult> GetClubPlayers(int? id)
        {
            var clubPlayers = clubsRepository.GetClubPlayers(id);

            if (clubPlayers == null || clubPlayers.Count() == 0) // it is never not null, it always has at least one, the owner
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
                var playerSuccessfullyRemoved = await clubsRepository.RemovePlayer(playerEmail, clubOwnerId);
                if (playerSuccessfullyRemoved)
                    return Ok();
                else
                    return StatusCode(500, new { Message = "Internal Server Error" });
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
                List<ApiUserDto> clubPlayers = await clubsRepository.GetClubPlayers(clubOwnerId, includeOwner);
                return Ok(clubPlayers);
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
                var requestDtos = await clubsRepository.GetClubJoinRequests(clubOwnerId);
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
                bool playerSuccessfullyInvited = await clubsRepository.InvitePlayerToJoinClub(clubOwnerId, gamingPlatformAccountId);
                if (playerSuccessfullyInvited)
                    return Ok("Player sucessfully invited.");
                else
                    return StatusCode(500, new { Message = "Internal Server Error" });
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
                List<ClubInviteDto> invitesDtos = await clubsRepository.GetClubJoinInvites(playerId);
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
                bool playerSuccessfullyAccepted = await clubsRepository.AcceptPlayerJoinRequest(requestId);
                if (playerSuccessfullyAccepted)
                    return Ok("Request sucessfully accepted.");
                else
                    return StatusCode(500, new { Message = "Internal Server Error" });
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
                bool playerSuccessfullyRejected = await clubsRepository.RejectPlayerJoinRequest(requestId);
                if (playerSuccessfullyRejected)
                    return Ok("Request sucessfully rejected.");
                else
                    return StatusCode(500, new { Message = "Internal Server Error" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
    }
}
