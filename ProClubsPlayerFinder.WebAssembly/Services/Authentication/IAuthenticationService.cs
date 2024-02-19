using ProClubsPlayerFinder.ClassLibrary.DTOs.ApiUserDTOs;
using ProClubsPlayerFinder.ClassLibrary.DTOs.ClassLibraryUserDTOs;

namespace ProClubsPlayerFinder.WebAssembly.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthResponse> LoginAsync(LoginUserDto loginUserDto);

        Task<HttpResponseMessage> RegisterAsync(ApiUserDto apiUserDto);
    }
}
