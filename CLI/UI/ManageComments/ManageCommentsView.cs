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

    public async Task EditAsync(Comment comment)
    {
        await commentRepository.UpdateAsync(comment);
        Console.WriteLine("Comment edited.");
    }
}