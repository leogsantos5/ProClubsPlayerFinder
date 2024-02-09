using System;
using System.Collections.Generic;

namespace ProClubsPlayerFinder.API.Data;

public partial class Club
{
    public int Id { get; set; }

    public string OwnerPlayerId { get; set; }

    public string? Description { get; set; }

    public string? ClubName { get; set; }

    public string? Console { get; set; }

    public virtual ApiUser OwnerPlayer { get; set; } = null!;

    public virtual ICollection<ApiUser> Players { get; set; } = new List<ApiUser>();
}
