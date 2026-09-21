using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class SingleCommentView(ICommentRepository commentRepository)
{
    public async Task SingleAsync(Comment comment)
    {
        await commentRepository.GetSingleAsync(comment.Id);
        Console.WriteLine($"Comment id: {comment.Id}");
        Console.WriteLine($"Body: {comment.Body}");
    }
}