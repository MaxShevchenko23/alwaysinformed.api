using System;
using System.Collections.Generic;

namespace alwaysinformed_dal.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int UserRole { get; set; }

    public string? UserPhoto { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual UserRole UserRoleNavigation { get; set; } = null!;
}
