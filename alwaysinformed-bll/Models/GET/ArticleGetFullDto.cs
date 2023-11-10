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

        public int? AuthorId { get; set; }

        public int? CategoryId { get; set; }

        public string Image { get; set; } = null!;

        public DateTime PublicationDate { get; set; }

        public string ShortDescription { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Url { get; set; } = null!;

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    }
}
