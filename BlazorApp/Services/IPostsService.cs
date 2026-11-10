using DTOs.Post;
using Entities;

namespace BlazorApp.Services;

public interface IPostsService
{
    public Task<PostDto> AddPostAsync(CreatePostDto request);
    public Task<PostDto> UpdatePostAsync(int id, UpdatePostDto request);
    public Task DeletePostAsync(int id);
    public Task<PostDto> GetPostAsync(int id);
    public Task<PostDto[]> GetPostsAsync();
}