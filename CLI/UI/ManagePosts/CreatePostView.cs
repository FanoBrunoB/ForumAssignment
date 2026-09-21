using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView(IPostRepository postRepository)
{
    public async Task CreatePost()
    {
        Console.Write("User id: ");
        int userId = int.Parse(Console.ReadLine()!);

        Console.Write("Forum id: ");
        int forumId = int.Parse(Console.ReadLine()!);

        Console.Write("Title: ");
        string title = Console.ReadLine()!;

        Console.Write("Body: ");
        string body = Console.ReadLine()!;

        Post post = new Post(userId, forumId, title, body);

        Post createdPost = await postRepository.AddAsync(post);

        Console.WriteLine(
            $"Post created: {createdPost.Title} (id: {createdPost.Id})");
    }
}