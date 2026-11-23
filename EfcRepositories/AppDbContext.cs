using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Reaction> Reactions => Set<Reaction>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Forum> Forums => Set<Forum>();

   
}