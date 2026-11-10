namespace Entities;

public enum ReactionType
{
    Like,
    Dislike
}

public class Reaction(int userId, ReactionType type) : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; } = userId;
    public ReactionType Type { get; set; } = type;
    public int? PostId { get; set; }
    public int? CommentId { get; set; }
    
}