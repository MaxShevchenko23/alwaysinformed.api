using System;
using System.Collections.Generic;

namespace alwaysinformed_dal.Entities;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public string UserRoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
