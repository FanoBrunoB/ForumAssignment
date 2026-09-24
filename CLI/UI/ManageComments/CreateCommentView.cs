using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository)
{
    public async Task CreateAsync()
    {
        User? user = null;
        Post? post = null;

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

        while (post == null)
        {
            Console.Write("Post id: ");
            int postId = int.Parse(Console.ReadLine()!);

            try
            {
                post = await postRepository.GetSingleAsync(postId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Post not found, try again.");
            }
        }
        

        Console.Write("Body: ");
        string body = Console.ReadLine()!;

        Comment comment = new Comment(user.Id, post.Id, body);

        Comment createdComment = await commentRepository.AddAsync(comment);

        Console.WriteLine($"Comment created (id: {createdComment.Id})");
    }
}