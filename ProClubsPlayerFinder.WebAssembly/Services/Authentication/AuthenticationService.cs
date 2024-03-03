using Blazored.LocalStorage;
using ProClubsPlayerFinder.WebAssembly.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClassLibraryUserDTOs;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ApiUserDTOs;


namespace ProClubsPlayerFinder.WebAssembly.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        //private readonly IClient iClient;
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly CustomAuthStateProvider customAuthStateProvider;

        public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorage, CustomAuthStateProvider customAuthStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.customAuthStateProvider = customAuthStateProvider;
        }
        public async Task<HttpResponseMessage> RegisterAsync(ApiUserDto apiUserDto)
        {
            return await httpClient.PostAsJsonAsync("api/auth/Register", apiUserDto);
        }

        public async Task<AuthResponse> LoginAsync(LoginUserDto loginUserDto)
        {
            var response = await httpClient.PostAsJsonAsync("api/Auth/Login", loginUserDto);     
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuthResponse>();         
        }

        public async Task LogoutAsync()
        {
            await customAuthStateProvider.LogOut();
        }

    }
}
