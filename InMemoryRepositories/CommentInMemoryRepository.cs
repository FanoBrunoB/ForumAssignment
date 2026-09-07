using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository: ICommentRepository
{
    private InMemoryRepository<Comment> comments = new();
    
    public Task<Comment> AddAsync(Comment comment)
    {
        return comments.AddAsync(comment);
    }
    
    public Task UpdateAsync(Comment comment)
    {
        return comments.UpdateAsync(comment);
    }
    
    public Task DeleteAsync(int id)
    {
        return comments.DeleteAsync(id);
    }
    
    public Task<Comment> GetSingleAsync(int id)
    {
        return comments.GetSingleAsync(id);
    }
    
    public IQueryable<Comment> GetMany()
    {
        return comments.GetMany();
    }
}