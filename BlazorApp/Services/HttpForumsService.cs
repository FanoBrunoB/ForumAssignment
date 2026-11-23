using System.Text.Json;
using DTOs.Forum;

namespace BlazorApp.Services;

public class HttpForumsService(HttpClient client) : IForumsService
{
    private readonly HttpClient client = client;

    public async Task<ForumDto> AddForumAsync(CreateForumDto request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("forums", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<ForumDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<ForumDto[]> GetForumsAsync()
    {
        HttpResponseMessage httpResponse = await client.GetAsync("forums");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<ForumDto[]>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}