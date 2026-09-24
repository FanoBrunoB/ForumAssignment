namespace Entities;

public class User(string username, string password) : IEntity
{
    public int Id { get; set; }
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public override string ToString()
    {
        return $"ID : {Id}, Username : {Username}";
    }
}