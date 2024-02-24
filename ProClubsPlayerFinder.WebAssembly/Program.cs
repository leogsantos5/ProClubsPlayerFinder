using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProClubsPlayerFinder.WebAssembly.Providers;
using ProClubsPlayerFinder.WebAssembly;
using ProClubsPlayerFinder.WebAssembly.Services.Authentication;
using System.IdentityModel.Tokens.Jwt;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped<IClient, Client>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7224") });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<JwtSecurityTokenHandler>();

builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

await builder.Build().RunAsync();
