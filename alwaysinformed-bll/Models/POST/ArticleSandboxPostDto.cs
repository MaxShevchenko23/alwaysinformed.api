using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Models.POST
{
    public class ArticleSandboxPostDto
    {
        public int AuthorId { get; set; }
        public string Content { get; set; } = null!;

        public int? CategoryId { get; set; }

        public string Image { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string Title { get; set; } = null!;
        public int ArticleStatus { get; set; }
        public string? AdminEmail { get; set; }

        public string? ArticleAdminComment { get; set; }


    }
}
