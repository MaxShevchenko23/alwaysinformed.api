using System;
using System.Collections.Generic;

namespace alwaysinformed_dal.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<ArticleSandbox> ArticleSandboxes { get; set; } = new List<ArticleSandbox>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
