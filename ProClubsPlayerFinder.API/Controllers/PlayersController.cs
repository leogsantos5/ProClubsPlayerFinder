using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProClubsPlayerFinder.API.Data;
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

        public PlayersController(UserManager<ApiUser> _userManager, ClubsPlayerFinderEafc24Context _context)
        {
            context = _context;
            userManager = _userManager;
        }

        // GET: All Free Agents
        [HttpGet("GetFreeAgents")]
        public async Task<ActionResult<IEnumerable<ApiUserDto>>> GetFreeAgents()
        {
            var freeAgentsDtos = await context.Players.Where(p => p.ClubId == null)
               .Select(player => new ApiUserDto
               {
                   Email = player.Email,
                   FirstName = player.FirstName,
                   LastName = player.LastName,
                   DateOfBirth = player.DateOfBirth,
                   Country = player.Country,
                   GamingPlatformAccountId = player.GamingPlatformAccountId,
                   Console = player.Console
               }).Take(10).ToListAsync(); // Only gets free agents

            return Ok(freeAgentsDtos); 
        }

        // Get: Gets a player
        [HttpGet("GetPlayer/{idOrEmail}")]
        public async Task<ActionResult<UpdatePlayerDto>> GetPlayer(string? idOrEmail)
        {
            ApiUser player = new();
            if (idOrEmail.Contains("@"))
                player = await context.Players.FirstOrDefaultAsync(c => c.Email == idOrEmail);
            else
                player = await context.Players.FirstOrDefaultAsync(c => c.Id == idOrEmail);
            if (player == null)
                return NotFound(); // Return a 404 Not Found if the player is not found

            UpdatePlayerDto apiUserDto = new UpdatePlayerDto
            {
                Country = player.Country,
                Console = player.Console,
                GamingPlatformAccountId = player.GamingPlatformAccountId,
                Description = player.Description!,
            };

            return Ok(apiUserDto);
        }
        
        [HttpPut("UpdatePlayer/{id}")]
        public async Task<IActionResult> UpdatePlayer(string id, UpdatePlayerDto updatePlayerDto)
        {
            try
            {
                var player = await context.Players.FindAsync(id);
                if (player == null)
                    return NotFound(); // Or handle the case where the club with the given id is not found

                player.Country = updatePlayerDto.Country;
                player.GamingPlatformAccountId = updatePlayerDto.GamingPlatformAccountId;
                player.Description = updatePlayerDto.Description;
                player.Console = updatePlayerDto.Console;

                await context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }
}
