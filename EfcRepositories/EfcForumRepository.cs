using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcForumRepository : IForumRepository
{
    private readonly AppDbContext ctx;

    public EfcForumRepository(AppDbContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Forum> AddAsync(Forum forum)
    {
        EntityEntry<Forum> entityEntry = await ctx.Forums.AddAsync(forum);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Forum forum)
    {
        if (!await ctx.Forums.AnyAsync(f => f.Id == forum.Id))
        {
            throw new InvalidOperationException($"Forum with id {forum.Id} not found");
        }

        ctx.Forums.Update(forum);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Forum? existing = await ctx.Forums.SingleOrDefaultAsync(f => f.Id == id);
        if (existing is null)
        {
            throw new InvalidOperationException($"Forum with id {id} not found");
        }

        ctx.Forums.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Forum> GetSingleAsync(int id)
    {
        Forum? forum = await ctx.Forums.SingleOrDefaultAsync(f => f.Id == id);
        if (forum is null)
        {
            throw new InvalidOperationException($"Forum with id {id} not found");
        }

        return forum;
    }

    public IQueryable<Forum> GetMany()
    {
        return ctx.Forums.AsQueryable();
    }
}