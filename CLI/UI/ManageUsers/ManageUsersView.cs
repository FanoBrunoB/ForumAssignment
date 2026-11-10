using RepositoryContracts;
using Entities;
namespace CLI.UI.ManageUsers;

public class ManageUsersView(IUserRepository userRepository)
{
    public async Task DeleteAsync(int userId)
    {
        await userRepository.DeleteAsync(userId);
        Console.WriteLine($"User {userId} has been deleted");
    }
}