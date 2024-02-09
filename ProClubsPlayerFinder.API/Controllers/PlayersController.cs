using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProClubsPlayerFinder.API.Data;
using ProClubsPlayerFinder.API.DTOs.PlayerDTOs;

namespace ProClubsPlayerFinder.API.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ClubsPlayerFinderEafc24Context _context;

        public PlayersController(ClubsPlayerFinderEafc24Context context)
        {
            _context = context;
        }

        // POST: Players/Create
        // To protect from overposting attacks, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Players/CreatePlayer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password,FirstName,LastName,DateOfBirth,PhoneNumber,Country,GamingPlatformAccountId,Console")] PlayerCreateDto playerToCreate)
        {
            var player = new ApiUser
            {
                Email = playerToCreate.Email,
                //PasswordHash = playerToCreate.Password, // falta função de hashing
                FirstName = playerToCreate.FirstName,
                LastName = playerToCreate.LastName,
                DateOfBirth = playerToCreate.DateOfBirth,
                PhoneNumber = playerToCreate.PhoneNumber,
                Country = playerToCreate.Country,
                GamingPlatformAccountId = playerToCreate.GamingPlatformAccountId,
                Console = playerToCreate.Console
            };

            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }        

    }
}
