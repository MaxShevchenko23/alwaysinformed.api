using alwaysinformed_dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Models.GET
{
    public class ArticleGetFullDto
    {
        public string Content { get; set; } = null!;

        public int ArticleId { get; set; }

        public int AuthorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int? CategoryId { get; set; }

        public string Image { get; set; } = null!;

        public DateTime PublicationDate { get; set; }

        public string ShortDescription { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Url { get; set; } = null!;
        public int? ArticleSandboxId { get; set; }


        public virtual IEnumerable<CommentGetDto>? Comments { get; set; }

        public virtual IEnumerable<Favorite>? Favorites { get; set; }

    }
}
