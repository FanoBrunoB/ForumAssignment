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
    public User User { get; set; } = null!;
    public Post? Post { get; set; }
    public Comment? Comment { get; set; }
    public ReactionType Type { get; set; } = type;
    public int? PostId { get; set; }
    public int? CommentId { get; set; }
    
}