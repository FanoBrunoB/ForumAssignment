using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView(IUserRepository userRepository)
{
    public IQueryable<User> GetAllUsers()
    {
        IQueryable<User> users = userRepository.GetMany();

        foreach (User user in users)
        {
            Console.WriteLine(user.Username);
        }

        return users;
    }

}