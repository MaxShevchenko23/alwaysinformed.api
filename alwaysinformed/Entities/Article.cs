using System.ComponentModel.DataAnnotations;

namespace alwaysinformed.Entities;

public class Article
{
    public int ArticleId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Author { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
    public string URL { get; set; }
    public string ShortDescription { get; set; }

    public ICollection<Comment> Comments { get; set; }
}
