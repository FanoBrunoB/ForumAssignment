using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class SingleUserView(IUserRepository userRepository)
{
    public async Task GetAsync(int id)
    {
        await userRepository.GetSingleAsync(id);
        Console.WriteLine("User deleted: " + id);
    }
}