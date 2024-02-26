using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProClubsPlayerFinder.API.Data;
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
        public async Task<ActionResult<IEnumerable<Club>>> GetClubs()
        {
            return Ok(await context.Clubs.ToListAsync());
        }

        // GET: Gets specific Club
        [HttpGet("GetClub/{id}")]
        public async Task<ActionResult<Club>> GetClub(int? id)
        {
            var club = await context.Clubs.Include(club => club.Players).FirstOrDefaultAsync(c => c.Id == id);

            if (club == null)
                return NotFound(); // Return a 404 Not Found if the club is not found

            return Ok(club);
        }

        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Free Agent")]
        // POST: Creates a Club
        [HttpPost("CreateClub")]
        public async Task<ActionResult<Club>> CreateClub([Bind("Description,ClubName")] ClubCreateDto clubToCreate)
        {
            string authenticatedUserId = User.Claims.FirstOrDefault(claim => claim.Type == "uid")!.Value;
            ApiUser playerOwner = userManager.FindByIdAsync(authenticatedUserId).Result!;
            // Create the club
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

            return Ok(club);           
        }

        // PUT: Changes Club info
        [HttpPut("ChangeClub/{id}")]
        [Authorize(Roles = "Club Owner")]
        public async Task<ActionResult> ChangeClub(int? id, Club clubInfoToUpdate)
        {
            if (id != clubInfoToUpdate.Id)
                return NotFound();

            var club = await context.Clubs.FirstOrDefaultAsync(c => c.Id == id);

            if (club == null)
                return NotFound(); // Return a 404 Not Found if the club is not found

            club.Description = clubInfoToUpdate.Description;
            club.Players = clubInfoToUpdate.Players;

            return Ok(club);
        }

        // DELETE: Deletes a Club
        [HttpDelete("DeleteClub/{id}")]
        [Authorize(Roles = "Club Owner")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var club = await context.Clubs.FindAsync(id);
            if (club != null)
            {
                context.Clubs.Remove(club);
                await context.SaveChangesAsync();
            }
            return NoContent();
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

        private bool ClubExists(int id)
        {
            return context.Clubs.Any(e => e.Id == id);
        }
    }
}
