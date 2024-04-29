using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProClubsPlayerFinder.API.Contracts;
using ProClubsPlayerFinder.API.Data;
using ProClubsPlayerFinder.API.Migrations;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ApiUserDTOs;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClassLibraryUserDTOs;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClubDTOs;

namespace ProClubsPlayerFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayersController : ControllerBase
    {
        private readonly ClubsPlayerFinderEafc24Context context;
        private readonly UserManager<ApiUser> userManager;
        private readonly IPlayersRepository playersRepository;

        public PlayersController(UserManager<ApiUser> _userManager, ClubsPlayerFinderEafc24Context _context, IPlayersRepository playersRepository)
        {
            context = _context;
            userManager = _userManager;
            this.playersRepository = playersRepository;
        }

        // GET: All Free Agents
        [HttpGet("GetFreeAgents")]
        public async Task<ActionResult<IEnumerable<ApiUserDto>>> GetFreeAgents()
        {           
            var freeAgentsDtos = playersRepository.GetFreeAgents();
            return Ok(freeAgentsDtos); 
        }

        // Get: Gets a player
        [HttpGet("GetPlayer/{idOrEmail}")]
        public async Task<ActionResult<UpdatePlayerDto>> GetPlayer(string? idOrEmail)
        {
            var apiUserDto = playersRepository.GetPlayer(idOrEmail).Result;
            if (apiUserDto == null)
                return NotFound();
            else
                return Ok(apiUserDto);
        }
        
        [HttpPut("UpdatePlayer/{id}")]
        public async Task<IActionResult> UpdatePlayer(string id, UpdatePlayerDto updatePlayerDto)
        {
            try
            {
                var playerUpdatedSuccessfully = await playersRepository.UpdatePlayer(id, updatePlayerDto);
                if (playerUpdatedSuccessfully)
                    return Ok("Player updated successfully.");
                else
                    return StatusCode(500, new { Message = "Internal Server Error" });
            }
            catch
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet("AcceptClubJoinInvite/{inviteId}")]
        [Authorize(Roles = "Free Agent")]
        public async Task<IActionResult> AcceptClubJoinInvite(int inviteId)
        {
            try
            {
                var clubInviteAccepted = await playersRepository.AcceptClubJoinInvite(inviteId);
                if (clubInviteAccepted)
                    return Ok("Invite sucessfully accepted.");
                else
                    return StatusCode(500, new { Message = "Internal Server Error" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        [HttpGet("RejectClubJoinInvite/{inviteId}")]
        [Authorize(Roles = "Free Agent")]
        public async Task<IActionResult> RejectClubJoinInvite(int inviteId)
        {
            try
            {
                var clubInviteRejected = await playersRepository.RejectClubJoinInvite(inviteId);
                if (clubInviteRejected)
                    return Ok("Invite sucessfully rejected.");
                else
                    return StatusCode(500, new { Message = "Internal Server Error" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        // Get: Requests to join a club
        [HttpGet("RequestToJoinClub/{clubId}/{playerId}")]
        [Authorize(Roles = "Free Agent")]
        public async Task<IActionResult> RequestToJoinClub(int clubId, string playerId)
        {
            try
            {
                var requestToJoinSuccessful = await playersRepository.RequestToJoinClub(clubId, playerId);
                if (requestToJoinSuccessful)
                    return Ok("Request sucessfully sent.");
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
