using alwaysinformed_dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Models.POST
{
    public class ArticleFilterDto
    {
        public string? Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public string? AuthorName { get; set; }
        public string? Category { get; set; }
    }
}
