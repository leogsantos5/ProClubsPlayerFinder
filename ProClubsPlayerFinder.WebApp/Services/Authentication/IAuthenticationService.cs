using ProClubsPlayerFinder.WebApp.Services.Base;

namespace ProClubsPlayerFinder.WebApp.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);

        public Task Logout();
    }
}
