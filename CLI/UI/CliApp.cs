using RepositoryContracts;
using CLI.UI.ManageUsers;
namespace CLI.UI;


public class CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository, IForumRepository forumRepository, IReactionRepository interactionRepository)
{
    public async Task StartAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Manage Users ===");
        Console.WriteLine("1. Create new user");
        Console.WriteLine("2. Update existing user");
        Console.WriteLine("3. Delete user");
        Console.WriteLine("4. See all users");
        Console.WriteLine("0. Back");
        Console.Write("Choose an option: ");
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await new CreateUserView(userRepository).CreateUser();
                break;
            case "4":
                 new ListUsersView(userRepository).GetAllUsers();
                 break;
        }
    }
}