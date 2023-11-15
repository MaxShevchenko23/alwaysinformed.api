using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Models.GET
{
    public class ArticleSandboxGetDto
    {
        public string Content { get; set; } = null!;

        public int SandboxId { get; set; }

        public int? AuthorId { get; set; }

        public int? CategoryId { get; set; }

        public string Image { get; set; } = null!;

        public DateTime PublicationDate { get; set; }

        public string ShortDescription { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Url { get; set; } = null!;

        public string? AdminEmail { get; set; }

        public int ArticleStatus { get; set; }

        public string? ArticleAdminComment { get; set; }
    }
}
