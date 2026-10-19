using Entities;

namespace DTOs.Reaction;

public class ReactionDto
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required ReactionType Type { get; set; }
    public int? PostId { get; set; }
    public int? CommentId { get; set; }
}