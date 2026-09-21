using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView(ICommentRepository commentRepository)
{
    public async Task CreateAsync()
    {
        Console.Write("User id: ");
        int userId = int.Parse(Console.ReadLine()!);

        Console.Write("Post id: ");
        int postId = int.Parse(Console.ReadLine()!);

        Console.Write("Body: ");
        string body = Console.ReadLine()!;

        Comment comment = new Comment(userId, postId, body);

        Comment createdComment = await commentRepository.AddAsync(comment);

        Console.WriteLine($"Comment created (id: {createdComment.Id})");
    }
}