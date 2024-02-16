using Blazored.LocalStorage;
using ProClubsPlayerFinder.WebApp.Providers;
using ProClubsPlayerFinder.WebApp.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace ProClubsPlayerFinder.WebApp.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(IClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<string> AuthenticateAsync(LoginUserDto loginModel)
        {
            var response = await httpClient.LoginAsync(loginModel);

            return response.Token;
            // Store Token
            //await localStorage.SetItemAsync("accessToken", response.Token);

            // Change auth state of app
            //await ((ApiAuthStateProvider)authenticationStateProvider).LoggedIn();
        }

        public async Task Logout()
        {
            // Change auth state of app
            //await ((ApiAuthStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}
