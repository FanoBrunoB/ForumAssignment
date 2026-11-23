using DTOs.Forum;

namespace BlazorApp.Services;

public interface IForumsService
{
    public Task<ForumDto> AddForumAsync(CreateForumDto request);
    public Task<ForumDto[]> GetForumsAsync();
}