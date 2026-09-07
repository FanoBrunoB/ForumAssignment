using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class ForumInMemoryRepository: IForumRepository
{
    private InMemoryRepository<Forum> forums = new();
    
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
