using System.Text.Json;
using DTOs;

namespace BlazorApp.Services;

public class HttpCommentsService(HttpClient client) : ICommentsService
{
    private readonly HttpClient client = client;
    
    public async Task<CommentDto> AddCommentAsync(CreateCommentDto request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("comments", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<CommentDto> UpdateCommentAsync(int id, CreateCommentDto request)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCommentAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<CommentDto> GetCommentAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<CommentDto[]> GetCommentsAsync()
    {
        HttpResponseMessage httpResponse = await client.GetAsync("comments");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto[]>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
    
    public async Task<CommentDto[]> GetCommentsByPostAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"comments?postId={id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto[]>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
    
}