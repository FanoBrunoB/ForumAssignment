using RepositoryContracts;
using Entities;
namespace CLI.UI.ManagePosts;

public class ManagePostView(IPostRepository postRepository)
{
    public async Task DeleteAsync(int postId)
    {
        await postRepository.DeleteAsync(postId);
        Console.WriteLine($"Post deleted {postId}");
    }

    public async Task EditAsync(int postId, string title, string content)
    {
        Post post = await postRepository.GetSingleAsync(postId);

        post.Title = title;
        post.Body = content;
        await postRepository.UpdateAsync(post); 
        Console.WriteLine("Post updated.");
    }
}