using Entities;

namespace DTOs.Reaction;

public class UpdateReactionDto
{
    public required ReactionType Type { get; set; }
}