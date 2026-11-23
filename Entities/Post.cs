namespace Entities;

public class Post(int userId, string title, string body, int forumId) : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; } = userId;
    public User User { get; set; } = null!;
    public int ForumId { get; set; } = forumId;
    public Forum Forum { get; set; } = null!;
    public string Title { get; set; } = title;
    public string Body { get; set; } = body;
    public List<Comment> Comments { get; set; } = [];
    public List<Reaction> Reactions { get; set; } = [];
}