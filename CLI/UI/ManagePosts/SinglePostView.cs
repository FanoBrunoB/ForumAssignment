using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView(IPostRepository postRepository)
{
    public async Task SingleAsync(int postId)
    {
        await postRepository.GetSingleAsync(postId);
        Console.WriteLine($"Post with id: {postId}.");
    }

}