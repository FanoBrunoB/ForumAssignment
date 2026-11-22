using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageComments;

public class ListCommentsView(ICommentRepository commentRepository)
{
    public void Run()
    {
        IQueryable<Comment> comments = commentRepository.GetMany();

        Console.WriteLine("=== All Comments ===");
        foreach (Comment comment in comments)
        {
            Console.WriteLine($"[{comment.Id}] {comment.Body}");
        }
    }
}