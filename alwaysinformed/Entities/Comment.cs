namespace alwaysinformed.Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public DateTime DateTime { get; set; }
    public string UserName { get; set; }

    // Внешний ключ для связи с публикацией
    public int ArticleId { get; set; }
    public Article Article { get; set; }
}
