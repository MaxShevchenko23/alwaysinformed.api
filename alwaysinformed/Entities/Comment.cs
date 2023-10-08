using System;
using System.Collections.Generic;

namespace alwaysinformed.Entities;

public partial class Comment
{
    public int CommentId { get; set; }

    public string Text { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public string UserName { get; set; } = null!;

    public int ArticleId { get; set; }

    public virtual Article Article { get; set; } = null!;
}
