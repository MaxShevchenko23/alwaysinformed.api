using System;
using System.Collections.Generic;

namespace alwaysinformed_dal.Entities;

public partial class ArticleStatistic
{
    public int StatisticId { get; set; }

    public int Likes { get; set; } = 0;

    public int Views { get; set; } = 0;

    public int ArticleId { get; set; }

    public virtual Article Article { get; set; } = null!;
}
