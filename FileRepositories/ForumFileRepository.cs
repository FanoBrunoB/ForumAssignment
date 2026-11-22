using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class ForumFileRepository: IForumRepository
{
    private FileRepositories<Forum> forums = 
        new("forum.json");
    
    public Task<Forum> AddAsync(Forum forum)
    {
        return forums.AddAsync(forum);
    }
    
    public Task UpdateAsync(Forum forum)
    {
        return forums.UpdateAsync(forum);
    }
    
    public Task DeleteAsync(int id)
    {
        return forums.DeleteAsync(id);
    }
    
    public Task<Forum> GetSingleAsync(int id)
    {
        return forums.GetSingleAsync(id);
    }
    
    public IQueryable<Forum> GetMany()
    {
        return forums.GetMany();
    }
}