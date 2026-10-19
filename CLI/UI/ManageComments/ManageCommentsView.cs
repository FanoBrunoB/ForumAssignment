using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageComments;

public class ManageCommentsView(ICommentRepository commentRepository)
{
    public async Task DeleteAsync(int id)
    {
        await commentRepository.DeleteAsync(id);
        Console.WriteLine("Comment deleted.");
    }

    public async Task EditAsync(int id, string body)
    {
        Comment comment = await commentRepository.GetSingleAsync(id);
        comment.Body = body;
        await commentRepository.UpdateAsync(comment);
        Console.WriteLine("Comment edited.");
    }
}