using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Models.GET
{
    public class UserGetDto
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string UserRole { get; set; } = null!;
    }
}
