namespace alwaysinformed.Entities;

public class Author
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }

    // Навигационное свойство для статей
    public ICollection<Article> Articles { get; set; }
}
