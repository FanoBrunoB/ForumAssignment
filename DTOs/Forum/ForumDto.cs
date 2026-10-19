namespace DTOs.Forum;

public class ForumDto
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required string UserName { get; set; }
    public required string Title { get; set; }
}