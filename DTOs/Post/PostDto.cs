namespace DTOs.Post;

public class PostDto
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required string UserName { get; set; }
    public required int? ForumId { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
}