namespace Entities;

public class Comment(int userId, int postId, string body) : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; } = userId;
    public User User { get; set; } = null!;
    public int PostId { get; set; } = postId;
    public Post Post { get; set; } = null!;
    public string Body { get; set; } = body;
    public List<Reaction> Reactions { get; set; } = [];   
}