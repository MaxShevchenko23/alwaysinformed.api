using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Models.UPDATE
{
    public class AuthorUpdateDto
    {
        public int AuthorId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Avatar { get; set; } = null!;

        public int UserId { get; set; }

    }
}
