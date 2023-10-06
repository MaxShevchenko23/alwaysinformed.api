namespace alwaysinformed.Entities;

public class Favorite
{
    public int FavoriteId { get; set; }

    // Внешний ключи для связей с пользователем и публикацией
    public int UserId { get; set; }
    public int ArticleId { get; set; }

    public User User { get; set; }
    public Article Article { get; set; }
}
