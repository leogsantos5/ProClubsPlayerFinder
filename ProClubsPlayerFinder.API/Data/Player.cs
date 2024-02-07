using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ProClubsPlayerFinder.API.Data;

public partial class Player : IdentityUser
{
    public int? ClubId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Country { get; set; }

    public string? GamingPlatformAccountId { get; set; }

    public string? Console { get; set; }

    public virtual Club? Club { get; set; }

}
