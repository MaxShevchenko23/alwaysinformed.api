using System;
using System.Collections.Generic;

namespace alwaysinformed.Entities;

public partial class Author
{
    public int AuthorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
