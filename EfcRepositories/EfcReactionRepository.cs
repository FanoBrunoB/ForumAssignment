using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcReactionRepository : IReactionRepository
{
    private readonly AppDbContext ctx;

    public EfcReactionRepository(AppDbContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Reaction> AddAsync(Reaction reaction)
    {
        EntityEntry<Reaction> entityEntry = await ctx.Reactions.AddAsync(reaction);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Reaction reaction)
    {
        if (!await ctx.Reactions.AnyAsync(r => r.Id == reaction.Id))
        {
            throw new InvalidOperationException($"Reaction with id {reaction.Id} not found");
        }

        ctx.Reactions.Update(reaction);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Reaction? existing = await ctx.Reactions.SingleOrDefaultAsync(r => r.Id == id);
        if (existing is null)
        {
            throw new InvalidOperationException($"Reaction with id {id} not found");
        }

        ctx.Reactions.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Reaction> GetSingleAsync(int id)
    {
        Reaction? reaction = await ctx.Reactions.SingleOrDefaultAsync(r => r.Id == id);
        if (reaction is null)
        {
            throw new InvalidOperationException($"Reaction with id {id} not found");
        }

        return reaction;
    }

    public IQueryable<Reaction> GetMany()
    {
        return ctx.Reactions.AsQueryable();
    }
}