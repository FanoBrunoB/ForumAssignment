using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class ReactionInMemoryRepository: IReactionRepository
{
    private InMemoryRepository<Reaction> reactions = new();
    
    public Task<Reaction> AddAsync(Reaction reaction)
    {
        return reactions.AddAsync(reaction);
    }
    
    public Task UpdateAsync(Reaction reaction)
    {
        return reactions.UpdateAsync(reaction);
    }
    
    public Task DeleteAsync(int id)
    {
        return reactions.DeleteAsync(id);
    }
    
    public Task<Reaction> GetSingleAsync(int id)
    {
        return reactions.GetSingleAsync(id);
    }
    
    public IQueryable<Reaction> GetMany()
    {
        return reactions.GetMany();
    }
}