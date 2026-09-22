using RepositoryContracts;
using CLI.UI.ManageUsers;
using CLI.UI.ManagePosts;
using CLI.UI.ManageComments;
using CLI.UI.ManageForums;

namespace CLI.UI;

public class CliApp(
    IUserRepository userRepository,
    ICommentRepository commentRepository,
    IPostRepository postRepository,
    IForumRepository forumRepository,
    IReactionRepository interactionRepository)
{
    public async Task StartAsync()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1. Manage users");
            Console.WriteLine("2. Manage posts");
            Console.WriteLine("3. Manage comments");
            Console.WriteLine("4. Manage forums");
            Console.WriteLine("5. Manage reactions");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ManageUsersMenuAsync();
                    break;
                case "2":
                    await ManagePostsMenuAsync();
                    break;
                case "3":
                    await ManageCommentsMenuAsync();
                    break;
                case "4":
                    await ManageForumsMenuAsync();
                    break;
                case "5":
                    await ManageReactionsMenuAsync();
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }

    // ===================== USERS =====================
    private async Task ManageUsersMenuAsync()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine();
            Console.WriteLine("=== Manage Users ===");
            Console.WriteLine("1. Create new user");
            Console.WriteLine("2. See all users");
            Console.WriteLine("3. List specific user");
            Console.WriteLine("0. Back");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await new CreateUserView(userRepository).CreateUser();
                    break;
                case "2":
                    new ListUsersView(userRepository).GetAllUsers();
                    break;
                case "3":
                    Console.Write("User id: ");
                    string? id = Console.ReadLine();
                    try
                    {
                        await new SingleUserView(userRepository).GetAsync(int.Parse(id));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }

    private async Task ManagePostsMenuAsync()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine();
            Console.WriteLine("=== Manage Posts ===");
            Console.WriteLine("1. Create new post");
            Console.WriteLine("2. See all posts");
            Console.WriteLine("3. Edit post");
            Console.WriteLine("4. Delete post");
            Console.WriteLine("0. Back");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await new CreatePostView(postRepository, userRepository).CreatePost();
                    break;
                case "2":
                    new ListPostsView(postRepository).GetMany();
                    break;
                case "3":
                    Console.Write("Post id to edit: ");
                    int editPostId = int.Parse(Console.ReadLine()!);
                    Console.Write("New title: ");
                    string newTitle = Console.ReadLine()!;
                    Console.Write("New body: ");
                    string newBody = Console.ReadLine()!;
                    await new ManagePostView(postRepository).EditAsync(editPostId, newTitle, newBody);
                    break;
                case "4":
                    Console.Write("Post id to delete: ");
                    int deletePostId = int.Parse(Console.ReadLine()!);
                    await new ManagePostView(postRepository).DeleteAsync(deletePostId);
                    break;
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
    private async Task ManageCommentsMenuAsync()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine();
            Console.WriteLine("=== Manage Comments ===");
            Console.WriteLine("1. Create new comment");
            Console.WriteLine("2. See all comments");
            Console.WriteLine("3. View single comment");
            Console.WriteLine("4. Edit comment");
            Console.WriteLine("5. Delete comment");
            Console.WriteLine("0. Back");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await new CreateCommentView(commentRepository, userRepository, postRepository).CreateAsync();
                    break;
                case "2":
                    new ListCommentsView(commentRepository).Run();
                    break;
                case "3":
                    Console.Write("Comment id: ");
                    int viewCommentId = int.Parse(Console.ReadLine()!);
                    await new SingleCommentView(commentRepository).RunAsync(viewCommentId);
                    break;
                case "4":
                    Console.Write("Comment id to edit: ");
                    int editCommentId = int.Parse(Console.ReadLine()!);
                    Console.Write("New body: ");
                    string newCommentBody = Console.ReadLine()!;
                    await new ManageCommentsView(commentRepository).EditAsync(editCommentId, newCommentBody);
                    break;
                case "5":
                    Console.Write("Comment id to delete: ");
                    int deleteCommentId = int.Parse(Console.ReadLine()!);
                    await new ManageCommentsView(commentRepository).DeleteAsync(deleteCommentId);
                    break;
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
    private async Task ManageForumsMenuAsync()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine();
            Console.WriteLine("=== Manage Forums ===");
            Console.WriteLine("1. Create new forum");
            Console.WriteLine("2. See all forums");
            Console.WriteLine("3. Edit forum");
            Console.WriteLine("4. Delete forum");
            Console.WriteLine("0. Back");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await new CreateForumView(forumRepository).CreateAsync();
                    break;
                case "2":
                    new ForumsListView(forumRepository).GetMany();
                    break;
                case "3":
                    Console.Write("Forum id to edit: ");
                    int editForumId = int.Parse(Console.ReadLine()!);
                    Console.Write("New title: ");
                    string newForumTitle = Console.ReadLine()!;
                    await new ManageForumView(forumRepository).EditAsync(editForumId, newForumTitle);
                    break;
                case "4":
                    Console.Write("Forum id to delete: ");
                    int deleteForumId = int.Parse(Console.ReadLine()!);
                    await new ManageForumView(forumRepository).DeleteAsync(deleteForumId);
                    break;
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
    private async Task ManageReactionsMenuAsync()
    {
        Console.WriteLine();
        Console.WriteLine("Reactions aren't implemented yet.");
        await Task.CompletedTask;
    }
}