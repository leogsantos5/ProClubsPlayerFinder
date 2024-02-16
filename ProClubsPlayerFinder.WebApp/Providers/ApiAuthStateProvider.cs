using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProClubsPlayerFinder.WebApp.Providers
{
    public class ApiAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Constants.JWTToken))
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var getUserClaims = DecryptToken(Constants.JWTToken);
                if (getUserClaims != null) return await Task.FromResult(new AuthenticationState(anonymous));

                var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch { return await Task.FromResult(new AuthenticationState(anonymous)); }

            //var user = new ClaimsPrincipal(new ClaimsIdentity());
            //var savedToken = await localStorage.GetItemAsync<string>("accessToken");
            //if (savedToken == null)
            //    return new AuthenticationState(user);
            //var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            //if (tokenContent.ValidTo <= DateTime.Now)
            //    return new AuthenticationState(user);
            //var claims = tokenContent.Claims;
            //user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            //return new AuthenticationState(user);           
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

            var name = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
            return new CustomUserClaims(name!.Value, email!.Value);
        }

        public void UpdateAuthenticationState(string jwtToken)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                Constants.JWTToken = jwtToken;
                var getUserClaims = DecryptToken(jwtToken);
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
                Constants.JWTToken = null;

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
