using DTOs.User;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto request);
    // public Task<UserDto> UpdateUserAsync(int id, UpdateUserDto request);
    public Task<UserDto[]>  GetUsersAsync();
}