namespace alwaysinformed.Entities;

public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    // Навигационное свойство для статей
    public ICollection<Article> Articles { get; set; }
}
