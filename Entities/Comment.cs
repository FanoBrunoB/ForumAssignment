namespace Entities;

public class Comment(int userId, int postId, string body) : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; } = userId;
    public int PostId { get; set; } = postId;
    public string Body { get; set; } = body;
    public List<Reaction> Reactions { get; set; } = [];   
}