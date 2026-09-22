using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView(IPostRepository postRepository, IUserRepository userRepository)
{
    public async Task CreatePost()
    {
        User? user = null;

        while (user == null)
        {
            Console.Write("User id: ");
            int userId = int.Parse(Console.ReadLine()!);

            try
            {
                user = await userRepository.GetSingleAsync(userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("User not found, try again.");
            }
        }
        
        Console.Write("Title: ");
        string title = Console.ReadLine()!;

        Console.Write("Body: ");
        string body = Console.ReadLine()!;

        Post post = new Post(user.Id, title, body);

        Post createdPost = await postRepository.AddAsync(post);

        Console.WriteLine(
            $"Post created: {createdPost.Title} (id: {createdPost.Id})");
    }
}