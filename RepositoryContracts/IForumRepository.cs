using Entities;

namespace RepositoryContracts;

public interface IForumRepository
{
    Task<Forum> AddAsync(Forum forum);
    Task UpdateAsync(Forum forum);
    Task DeleteAsync(int id);
    Task<Forum> GetSingleAsync(int id);
    IQueryable<Forum> GetMany();
}