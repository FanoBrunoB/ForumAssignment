namespace DTOs.Post;

public class CreatePostDto
{
    public required int UserId { get; set; }
    public required int ForumId { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
}