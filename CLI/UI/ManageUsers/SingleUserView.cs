using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class SingleUserView(IUserRepository userRepository)
{
    public async Task GetAsync(int id)
    {
        User? user = await userRepository.GetSingleAsync(id);
        if (user == null) Console.WriteLine("User not founded");
        else Console.WriteLine(user);
    }
}