using RepositoryContracts;
using Entities;
namespace CLI.UI.ManagePosts;

public class ListPostsView(IPostRepository postRepository)
{
    public Task GetMany()
    {
        IQueryable<Post> posts = postRepository.GetMany();

        Console.WriteLine("=== All Posts ===");
        foreach (Post post in posts)
        {
            Console.WriteLine($"[{post.Id}] {post.Title}");
        }

        return Task.CompletedTask;
    } 
}