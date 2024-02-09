using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProClubsPlayerFinder.API.Data;
using ProClubsPlayerFinder.API.DTOs.ClubDTOs;
using static System.Reflection.Metadata.BlobBuilder;

namespace ProClubsPlayerFinder.API.Controllers
{
    public class ClubsController : Controller
    {
        private readonly ClubsPlayerFinderEafc24Context _context;

        public ClubsController(ClubsPlayerFinderEafc24Context context)
        {
            _context = context;
        }

        // GET: All Clubs
        [HttpGet("Clubs/GetClubs")]
        public async Task<ActionResult<IEnumerable<Club>>> GetClubs()
        {
            return Ok(await _context.Clubs.ToListAsync());
        }

        // GET: Gets specific Club
        [HttpGet("Clubs/GetClub/{id}")]
        public async Task<ActionResult<Club>> GetClub(int? id)
        {
            var club = await _context.Clubs.Include(club => club.Players).FirstOrDefaultAsync(c => c.Id == id);

            if (club == null)
                return NotFound(); // Return a 404 Not Found if the club is not found

            return Ok(club);
        }

        // POST: Creates a Club
        // To protect from overposting attacks, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Clubs/CreateClub")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Club>> CreateClub([Bind("OwnerPlayerId,Description,ClubName,Console")] ClubCreateDto clubToCreate)
        {
            var club = new Club
            {
                Description = clubToCreate.Description,
                OwnerPlayerId = clubToCreate.OwnerPlayerId.ToString(),
                ClubName = clubToCreate.ClubName,
                Console = clubToCreate.Console,
                Players = new List<ApiUser>()
            };

            ApiUser? playerOwner = await _context.Players.FirstOrDefaultAsync(player => player.Id == clubToCreate.OwnerPlayerId.ToString());
            if (playerOwner == null)
                return NotFound("Could not find a player with that id");
            else
                club.OwnerPlayer = playerOwner;

            _context.Clubs.Add(club);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClub), new { id = club.Id }, club);
        }

        // PUT: Changes Club info
        [HttpPut("Clubs/ChangeClub/{id}")]
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
        [HttpDelete("Clubs/DeleteClub/{id}")]
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
        [HttpGet("Clubs/GetClubPlayers/{id}")]
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
