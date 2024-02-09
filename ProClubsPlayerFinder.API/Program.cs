using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProClubsPlayerFinder.API.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("EAFC24PlayerClubsFinderDbConnection");
builder.Services.AddDbContext<ClubsPlayerFinderEafc24Context>(options => options.UseSqlServer(connString));
builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores< ClubsPlayerFinderEafc24Context>();

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b=> b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
