using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Models.UPDATE
{
    public class CategoryUpdateDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

    }
}
