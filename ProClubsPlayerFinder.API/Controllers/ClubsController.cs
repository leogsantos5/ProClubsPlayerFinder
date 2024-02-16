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
using ProClubsPlayerFinder.API.DTOs.ClubDTOs;
using static System.Reflection.Metadata.BlobBuilder;

namespace ProClubsPlayerFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClubsController : ControllerBase
    {
        private readonly ClubsPlayerFinderEafc24Context _context;
        private readonly UserManager<ApiUser> _userManager;

        public ClubsController(UserManager<ApiUser> userManager, ClubsPlayerFinderEafc24Context context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: All Clubs
        [HttpGet("GetClubs")]
        public async Task<ActionResult<IEnumerable<Club>>> GetClubs()
        {
            return Ok(await _context.Clubs.ToListAsync());
        }

        // GET: Gets specific Club
        [HttpGet("GetClub/{id}")]
        public async Task<ActionResult<Club>> GetClub(int? id)
        {
            var club = await _context.Clubs.Include(club => club.Players).FirstOrDefaultAsync(c => c.Id == id);

            if (club == null)
                return NotFound(); // Return a 404 Not Found if the club is not found

            return Ok(club);
        }

        // POST: Creates a Club
        // To protect from overposting attacks, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("CreateClub")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Free Agent")]
        public async Task<ActionResult<Club>> CreateClub([Bind("OwnerPlayerId,Description,ClubName,Console")] ClubCreateDto clubToCreate)
        {
            // Get the authenticated user
            var authenticatedPlayer = await _userManager.GetUserAsync(User);
            if (authenticatedPlayer == null)
                return Unauthorized("Player not authenticated");

            //if (await _userManager.IsInRoleAsync(authenticatedPlayer, "Club Owner"))
            //    return Forbid("You already are a Club Owner. You have to delete this club to create another club.");
            //else if (await _userManager.IsInRoleAsync(authenticatedPlayer, "Player"))
            //    return Forbid("You already are a Player. You have to leave this club to create your club.");

            var playerOwner = await _userManager.FindByIdAsync(clubToCreate.OwnerPlayerId.ToString());
            if (playerOwner == null)
                return NotFound("Could not find a player with that id");

            await _userManager.RemoveFromRoleAsync(playerOwner, "Free Agent");
            await _userManager.AddToRoleAsync(playerOwner, "Club Owner");

            var club = new Club
            {
                Description = clubToCreate.Description,
                OwnerPlayerId = clubToCreate.OwnerPlayerId.ToString(),
                ClubName = clubToCreate.ClubName,
                Console = clubToCreate.Console,
                Players = new List<ApiUser>()
            };

            club.OwnerPlayer = playerOwner;
            club.Players.Add(playerOwner);

            _context.Clubs.Add(club);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClub), new { id = club.Id }, club);
        }

        // PUT: Changes Club info
        [HttpPut("ChangeClub/{id}")]
        [Authorize(Roles = "Club Owner")]
        public async Task<ActionResult> ChangeClub(int? id, Club clubInfoToUpdate)
        {
            if (id != clubInfoToUpdate.Id)
                return NotFound();

            var club = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == id);

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
            var club = await _context.Clubs.FindAsync(id);
            if (club != null)
            {
                _context.Clubs.Remove(club);
                await _context.SaveChangesAsync();
            }
            return NoContent();          
        }

        // GET: Gets players of a Club
        [HttpGet("GetClubPlayers/{id}")]
        public async Task<IActionResult> GetClubPlayers(int? id)
        {
            var clubPlayers = await _context.Clubs.Where(club => club.Id == id).SelectMany(club => club.Players).ToListAsync();

            if (clubPlayers == null || clubPlayers.Count == 0) // it is never not null, it always has at least one, the owner
                return NotFound();

            return Ok(clubPlayers);
        }

        private bool ClubExists(int id)
        {
            return _context.Clubs.Any(e => e.Id == id);
        }
    }
}
