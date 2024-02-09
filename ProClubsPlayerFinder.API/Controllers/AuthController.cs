using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProClubsPlayerFinder.API.Data;
using ProClubsPlayerFinder.API.DTOs.ApiUserDTOs;

namespace ProClubsPlayerFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly ClubsPlayerFinderEafc24Context context;
        private readonly UserManager<ApiUser> userManager;

        public AuthController(UserManager<ApiUser> _userManager, ClubsPlayerFinderEafc24Context _context)
        {
            context = _context;
            userManager = _userManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(ApiUserDto apiUserDto)
        {
            var player = new ApiUser
            {
                Email = apiUserDto.Email,
                UserName = apiUserDto.Email,
                FirstName = apiUserDto.FirstName,
                LastName = apiUserDto.LastName,
                DateOfBirth = apiUserDto.DateOfBirth,
                Country = apiUserDto.Country,
                Console = apiUserDto.Console,
                GamingPlatformAccountId = apiUserDto.GamingPlatformAccountId,               
            };
            var result = await userManager.CreateAsync(player, apiUserDto.Password);

            if (result.Succeeded == false) {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(apiUserDto.Role))
                await userManager.AddToRoleAsync(player, "Free Agent");
            else
                await userManager.AddToRoleAsync(player, apiUserDto.Role);

            await userManager.AddToRoleAsync(player, apiUserDto.Role);
            return Accepted();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserDto apiUserDto)
        {
            var player = await userManager.FindByEmailAsync(apiUserDto.Email);
            if (player == null)
                return NotFound();

            var passwordValid = await userManager.CheckPasswordAsync(player, apiUserDto.Password);

            if (player == null || passwordValid == false)
                return Unauthorized(apiUserDto);

            return Accepted();
        }
    }
}
