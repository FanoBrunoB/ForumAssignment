using System.Text.Json;
using DTOs.User;

namespace BlazorApp.Services;

public class HttpUserService(HttpClient client) : IUserService
{
    public async Task<UserDto> AddUserAsync(CreateUserDto request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    // public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto request)
    // {
    //     HttpResponseMessage httpResponse = await client.PutAsJsonAsync("users", request);
    //     string response = await httpResponse.Content.ReadAsStringAsync();
    //     if (!httpResponse.IsSuccessStatusCode)
    //     {
    //         throw new Exception(response);
    //     }
    //     return JsonSerializer.Deserialize<UserDto>(response, new JsonSerializerOptions
    //     {
    //         PropertyNameCaseInsensitive = true
    //     })!;
    // }

    public async Task<UserDto[]> GetUsersAsync()
    {
        HttpResponseMessage httpResponse = await client.GetAsync("users");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDto[]>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}