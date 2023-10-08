using System;
using System.Collections.Generic;

namespace alwaysinformed.Entities;

public partial class Article
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

    public virtual Author? Author { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
