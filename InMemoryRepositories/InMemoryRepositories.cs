using Entities;

namespace InMemoryRepositories;

public class InMemoryRepository<T> where T : class, IEntity
{
    private readonly List<T> entities = [];

    public Task<T> AddAsync(T entity)
    {
        entity.Id = entities.Any()
            ? entities.Max(e => e.Id) + 1
            : 1;

        entities.Add(entity);

        return Task.FromResult(entity);
    }

    public Task UpdateAsync(T entity)
    {
        T? existingEntity = entities
            .SingleOrDefault(e => e.Id == entity.Id);

        if (existingEntity is null)
        {
            throw new InvalidOperationException(
                $"Entity with ID '{entity.Id}' not found");
        }

        entities.Remove(existingEntity);
        entities.Add(entity);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        T? entityToRemove = entities
            .SingleOrDefault(e => e.Id == id);

        if (entityToRemove is null)
        {
            throw new InvalidOperationException(
                $"Entity with ID '{id}' not found");
        }

        entities.Remove(entityToRemove);

        return Task.CompletedTask;
    }

    public Task<T> GetSingleAsync(int id)
    {
        T? entity = entities
            .SingleOrDefault(e => e.Id == id);

        if (entity is null)
        {
            throw new InvalidOperationException(
                $"Entity with ID '{id}' not found");
        }

        return Task.FromResult(entity);
    }

    public IQueryable<T> GetMany()
    {
        return entities.AsQueryable();
    }
}