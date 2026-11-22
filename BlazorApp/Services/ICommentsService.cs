using DTOs;
using Entities;

namespace BlazorApp.Services;

public interface ICommentsService
{
    public Task<CommentDto> AddCommentAsync(CreateCommentDto request);
    public Task<CommentDto> UpdateCommentAsync(int id, CreateCommentDto request);
    public Task DeleteCommentAsync(int id);
    public Task<CommentDto> GetCommentAsync(int id);
    public Task<CommentDto[]> GetCommentsAsync();
    public Task<CommentDto[]> GetCommentsByPostAsync(int id);
}