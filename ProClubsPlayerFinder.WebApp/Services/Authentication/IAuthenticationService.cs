using ProClubsPlayerFinder.WebApp.Services.Base;

namespace ProClubsPlayerFinder.WebApp.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateAsync(LoginUserDto loginModel);

        public Task Logout();
    }
}
