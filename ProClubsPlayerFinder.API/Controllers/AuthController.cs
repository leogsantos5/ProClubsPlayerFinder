using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using ProClubsPlayerFinder.API.Data;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ApiUserDTOs;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClassLibraryUserDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProClubsPlayerFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApiUser> userManager;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<ApiUser> _userManager, IConfiguration _configuration)
        {
            //context = _context;
            userManager = _userManager;
            configuration = _configuration;
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

            await userManager.AddToRoleAsync(player, apiUserDto.Role);
            return Accepted();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDto apiUserDto)
        {
            var player = await userManager.FindByEmailAsync(apiUserDto.Email);
            if (player == null)
                return NotFound();

            var passwordValid = await userManager.CheckPasswordAsync(player, apiUserDto.Password);

            if (player == null || passwordValid == false)
                return Unauthorized(apiUserDto);

            var tokenString = await GenerateTokenAsync(player);

            var response = new AuthResponse
            {
                Email = apiUserDto.Email,
                Token = tokenString,
                UserId = player.Id
            };

            return response;
        }

        private async Task<string> GenerateTokenAsync(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var userClaims = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["JwtSettings:Duration"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
