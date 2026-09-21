using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class SingleCommentView(ICommentRepository commentRepository)
{
    public async Task RunAsync(int id)
    {
        Comment comment = await commentRepository.GetSingleAsync(id);
        Console.WriteLine($"Comment id: {comment.Id}");
        Console.WriteLine($"Body: {comment.Body}");
    }
}