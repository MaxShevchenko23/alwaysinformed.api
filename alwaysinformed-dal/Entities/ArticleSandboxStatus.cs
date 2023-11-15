using System;
using System.Collections.Generic;

namespace alwaysinformed_dal.Entities;

public partial class ArticleSandboxStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<ArticleSandbox> ArticleSandboxes { get; set; } = new List<ArticleSandbox>();
}
