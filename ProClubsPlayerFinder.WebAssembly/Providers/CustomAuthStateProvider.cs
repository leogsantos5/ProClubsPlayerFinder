using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProClubsPlayerFinder.WebAssembly.Providers
{
    public class CustomAuthStateProvider(ILocalStorageService localStorageService) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var stringToken = await localStorageService.GetItemAsStringAsync("token");
                if (string.IsNullOrEmpty(stringToken))
                    return await Task.FromResult(new AuthenticationState(anonymous));
                else
                {
                    var getUserClaims = DecryptToken(stringToken);
                    if (getUserClaims != null) return await Task.FromResult(new AuthenticationState(anonymous));

                    var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                    return await Task.FromResult(new AuthenticationState(claimsPrincipal));
                }
            }
            catch { return await Task.FromResult(new AuthenticationState(anonymous)); }          
        }

        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new (ClaimTypes.Name, claims.Name!),
                    new Claim(ClaimTypes.Email, claims.Email!),
                }, "JwtAuth"));
        }

        private static CustomUserClaims DecryptToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var name = token.Claims.FirstOrDefault(claim => claim.Type == "sub");
            var email = token.Claims.FirstOrDefault(claim => claim.Type == "email");
            return new CustomUserClaims(name!.Value, email!.Value);
        }

        public async Task UpdateAuthenticationState(string jwtToken)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                var getUserClaims = DecryptToken(jwtToken);
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

        //public async Task LoggedIn()
        //{
        //    var savedToken = await localStorage.GetItemAsync<string>("accessToken");
        //    var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        //    var claims = tokenContent.Claims.ToList();
        //    claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        //    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        //    var authState = Task.FromResult(new AuthenticationState(user)); 
        //    NotifyAuthenticationStateChanged(authState);
        //}

        //public async Task LoggedOut()
        //{
        //    await localStorage.RemoveItemAsync("accessToken");
        //    var nobody = new ClaimsPrincipal(new ClaimsIdentity());
        //    var authState = Task.FromResult(new AuthenticationState(nobody));
        //    NotifyAuthenticationStateChanged(authState);
        //}
    }
}
