using System.Text.Json;
using Entities;

namespace FileRepositories;

public class FileRepositories<T> where T : class, IEntity
{
    
    private readonly string filePath;

    public FileRepositories(String entityFilePath)
    {
        filePath = entityFilePath;
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    private async Task<List<T>> LoadAsync()
    {
        string json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<T>>(json)!;
    }
    
    private async Task SaveAsync(List<T> entities)
    {
        string json = JsonSerializer.Serialize(entities);
        await File.WriteAllTextAsync(filePath, json);
    }
    
    public async Task<T> AddAsync(T entity)
    {
        List<T> entities = await LoadAsync();
        
        int maxId = entities.Count > 0 ? entities.Max(c => c.Id) : 1;
        entity.Id = maxId + 1;
        entities.Add(entity);
        
        await SaveAsync(entities);
        
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        List<T> entities = await LoadAsync();
        
        T? existingEntity = entities
            .SingleOrDefault(e => e.Id == entity.Id);
        
        if (existingEntity is null)
        {
            throw new InvalidOperationException(
                $"Entity with ID '{entity.Id}' not found");
        }
        entities.Remove(existingEntity);
        entities.Add(entity);
        
        await SaveAsync(entities);
    }
    
    public async Task DeleteAsync(int id)
    {
        List<T> entities = await LoadAsync();
        
        T? entityToRemove = entities
            .SingleOrDefault(e => e.Id == id);

        if (entityToRemove is null)
        {
            throw new InvalidOperationException(
                $"Entity with ID '{id}' not found");
        }
        entities.Remove(entityToRemove);
        
        await SaveAsync(entities);
    }

    public async Task<T> GetSingleAsync(int id)
    {
        List<T> entities = await LoadAsync();
        
        T? entity = entities
            .SingleOrDefault(e => e.Id == id);

        if (entity is null)
        {
            throw new InvalidOperationException(
                $"Entity with ID '{id}' not found");
        }

        return entity;
    }
    
    public IQueryable<T> GetMany() {
        string entitiesAsJson = File.ReadAllTextAsync(filePath).Result;
        List<T> entities = JsonSerializer.Deserialize<List<T>>(entitiesAsJson)!;
        return entities.AsQueryable();
    }
    
}