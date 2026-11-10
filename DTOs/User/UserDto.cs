namespace DTOs.User;

public class UserDto
{
    public required int Id { get; set; }
    public required string UserName { get; set; }
    public string Password { get; set; }
}