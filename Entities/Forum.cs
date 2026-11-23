namespace Entities;

public class Forum(int userId, string title) : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; } = userId;
    public User User { get; set; } = null!;
    public List<Post> Posts { get; set; } = [];
    public string Title { get; set; } = title;
}