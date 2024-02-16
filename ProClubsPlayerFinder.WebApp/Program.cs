using Blazored.LocalStorage;
using ProClubsPlayerFinder.WebApp.Components;
using ProClubsPlayerFinder.WebApp.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using ProClubsPlayerFinder.WebApp.Providers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProClubsPlayerFinder.WebApp.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("https://localhost:7224"));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ApiAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<ApiAuthStateProvider>());

var app = builder.Build();

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
//The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
.AddInteractiveServerRenderMode();

app.Run();


//var builder = WebAssemblyHostBuilder.CreateDefault(args);
//builder.RootComponents.Add<App>("#app");
//builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7224") });

//builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddScoped<ApiAuthStateProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<ApiAuthStateProvider>());
//builder.Services.AddAuthorizationCore();

//builder.Services.AddScoped<IClient, Client>();
//builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

//await builder.Build().RunAsync();
