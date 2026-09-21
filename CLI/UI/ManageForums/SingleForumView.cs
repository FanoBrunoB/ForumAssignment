using RepositoryContracts;

namespace CLI.UI.ManageForums;

public class SingleForumView(IForumRepository forumRepository)
{
    private async Task getAsync(int forumId)
    {
        await forumRepository.GetSingleAsync(forumId);
        Console.WriteLine($"Forum {forumId} has been found");
    }
}