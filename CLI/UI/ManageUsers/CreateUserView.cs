using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageUsers;

public class CreateUserView(IUserRepository userRepository)
{
    public async Task CreateUser()
    {
        Console.Write("Username: ");
        string? username = Console.ReadLine();
        bool usernameExists = userRepository.GetMany().Any(u => u.Username == username);

        if (usernameExists)
        {
            Console.WriteLine("That username is already taken.");
            return;
        }
        Console.Write("Password: ");
        string? password = Console.ReadLine();
        
        
        User user = new User(username, password)
        {
            Username = username,
            Password = password
        };
        
        
        await userRepository.AddAsync(user);
        
        Console.Write("Created user: " + user);
    }
}
