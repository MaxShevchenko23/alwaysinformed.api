using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Models.GET
{
    public class FavoriteGetDto
    {
        public int FavoriteId { get; set; }

        public int UserId { get; set; }

        public int ArticleId { get; set; }
    }
}
