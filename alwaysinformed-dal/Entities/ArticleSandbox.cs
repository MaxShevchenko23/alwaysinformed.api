﻿using System;
using System.Collections.Generic;

namespace alwaysinformed_dal.Entities;

public partial class ArticleSandbox
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
}
