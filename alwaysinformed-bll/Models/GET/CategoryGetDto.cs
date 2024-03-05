using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Models.GET
{
    public class CategoryGetDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;
    }
}
