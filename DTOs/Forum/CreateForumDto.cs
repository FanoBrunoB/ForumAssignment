namespace DTOs.Forum;

public class CreateForumDto
{
    public required int UserId { get; set; }
    public required string Title { get; set; }
}