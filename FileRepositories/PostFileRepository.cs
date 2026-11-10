using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private FileRepositories<Post> posts = 
        new("posts.json");
    
    public Task<Post> AddAsync(Post post)
    {
        return posts.AddAsync(post);
    }
    
    public Task UpdateAsync(Post post)
    {
        return posts.UpdateAsync(post);
    }
    
    public Task DeleteAsync(int id)
    {
        return posts.DeleteAsync(id);
    }
    
    public Task<Post> GetSingleAsync(int id)
    {
        return posts.GetSingleAsync(id);
    }
    
    public IQueryable<Post> GetMany()
    {
        return posts.GetMany();
    }
}