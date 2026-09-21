using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ListCommentsView(ICommentRepository commentRepository)
{
    private readonly ICommentRepository commentRepository = commentRepository;

    public Task ListAllComments()
    {
        commentRepository.GetMany();
        return Task.CompletedTask;
    }
}