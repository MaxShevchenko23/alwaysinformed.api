using alwaysinformed_dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Models.UPDATE
{
    public class UserUpdateDto
    {
        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? PasswordHash { get; set; }

        public string? Email { get; set; } = null!;

        public int? UserRole { get; set; } = 8;
        public string? UserPhoto { get; set; }

    }
}
