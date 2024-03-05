using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Models.GET
{
    public class ArticleStatisticGetDto
    {
        public int Likes { get; set; }

        public int Views { get; set; }

        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
    }
}
