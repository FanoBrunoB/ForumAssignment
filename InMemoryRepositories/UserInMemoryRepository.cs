using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository: IUserRepository
{
    private InMemoryRepository<User> users = new();
    
    public Task<User> AddAsync(User user)
    {
        return users.AddAsync(user);
    }
    
    public Task UpdateAsync(User user)
    {
        return users.UpdateAsync(user);
    }
    
    public Task DeleteAsync(int id)
    {
        return users.DeleteAsync(id);
    }
    
    public Task<User> GetSingleAsync(int id)
    {
        return users.GetSingleAsync(id);
    }
    
    public IQueryable<User> GetMany()
    {
        return users.GetMany();
    }
}