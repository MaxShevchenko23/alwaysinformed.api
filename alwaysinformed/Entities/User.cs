namespace alwaysinformed.Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string UserRole { get; set; }

    // Навигационное свойство для избранного
    public ICollection<Favorite> Favorites { get; set; }
}
