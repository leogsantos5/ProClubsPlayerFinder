using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using ProClubsPlayerFinder.ClassLibrary;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.Data;

namespace ProClubsPlayerFinder.WebAssembly.Providers
{
    public class CustomAuthStateProvider(ILocalStorageService localStorageService, JwtSecurityTokenHandler jwtSecurityTokenHandler) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        //https://youtu.be/P0XqYbUmSDU?si=Wx1IzYj1mdjJ9SVc e Trevoir (curso q fiz)
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var anonymousAuthState = await Task.FromResult(new AuthenticationState(anonymous));
                var stringToken = await localStorageService.GetItemAsStringAsync("token");
                var jwtTokenContent = jwtSecurityTokenHandler.ReadJwtToken(stringToken);

                if (string.IsNullOrEmpty(stringToken))
                    return anonymousAuthState;
                else if (DateTime.Now > jwtTokenContent.ValidTo)
                    return anonymousAuthState; // dizer que o token expirou
                else
                {
                    var getUserClaims = GetCustomUserClaimsFromToken(stringToken, "");
                    if (getUserClaims == null)
                        return await Task.FromResult(new AuthenticationState(anonymous));

                    var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                    return await Task.FromResult(new AuthenticationState(claimsPrincipal));
                }
            }
            catch { return await Task.FromResult(new AuthenticationState(anonymous)); }
        }

        public async Task UpdateAuthenticationState(string jwtToken)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                var getUserClaims = GetCustomUserClaimsFromToken(jwtToken, "");
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
                await localStorageService.SetItemAsStringAsync("token", jwtToken);
            }
            else
            {
                claimsPrincipal = anonymous;
                await localStorageService.RemoveItemAsync("token");
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task UpdateNewAuthStateWithChangedRole(string jwtToken, string newRole)
        {
            var newUserClaims = GetCustomUserClaimsFromToken(jwtToken, newRole);
            var newClaimsPrincipal = SetClaimPrincipal(newUserClaims);
            await localStorageService.SetItemAsStringAsync("token", jwtToken);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(newClaimsPrincipal)));
        }

        public async Task LogOut()
        {
            await localStorageService.RemoveItemAsync("token");
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }

        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new (ClaimTypes.Name, claims.Name!),
                    new Claim(ClaimTypes.Email, claims.Email!),
                    new Claim(ClaimTypes.Role, claims.Role!),
                }, "JwtAuth"));
        }

        private static CustomUserClaims GetCustomUserClaimsFromToken(string jwtToken, string newRole)
        {
            if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();

            var handler = new JwtSecurityTokenHandler();
            var tokenContent = handler.ReadJwtToken(jwtToken);
            var name = tokenContent.Claims.FirstOrDefault(claim => claim.Type == "sub");
            var email = tokenContent.Claims.FirstOrDefault(claim => claim.Type == "email");
            var role = newRole;

            if (role == "")
                role = tokenContent.Claims.FirstOrDefault(claim => claim.Type == Roles.RoleDictKey)!.Value;

            return new CustomUserClaims(name!.Value, email!.Value, role);
        }

        public string GetUserIdFromToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenContent = handler.ReadJwtToken(jwtToken);
            var userId = tokenContent.Claims.FirstOrDefault(claim => claim.Type == "uid")!.Value;
            return userId;
        }   
    }
}
