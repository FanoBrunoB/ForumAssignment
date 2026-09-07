namespace Entities;

public class Post(int userId, int forumId, string title, string body) : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; } = userId;
    public int ForumId { get; set; }
    public string Title { get; set; } = title;
    public string Body { get; set; } = body;
    public List<Reaction> Reactions { get; set; } = [];
}