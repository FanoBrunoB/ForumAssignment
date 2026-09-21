using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView(ICommentRepository commentRepository)
{
    private readonly ICommentRepository commentRepository = commentRepository;


    public async Task CreateComment(Comment comment)
    {
       await commentRepository.AddAsync(comment);
       
    }
}