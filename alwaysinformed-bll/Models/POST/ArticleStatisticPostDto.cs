using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Models.POST
{
    public class ArticleStatisticPostDto
    {
        public int StatisticId { get; set; }

        public int Likes { get; set; }

        public int Views { get; set; }

        public int ArticleId { get; set; }
    }
}
