﻿using System;
using System.Collections.Generic;

namespace alwaysinformed_dal.Entities;

public partial class Author
{
    public int AuthorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<ArticleSandbox> ArticleSandboxes { get; set; } = new List<ArticleSandbox>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual User User { get; set; } = null!;
}
