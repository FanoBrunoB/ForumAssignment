using RepositoryContracts;

namespace CLI.UI.ManageForums;
using Entities;
public class ManageForumView(IForumRepository forumRepository)
{
    public async Task EditAsync(int forumId, string title)
    {
        Forum forum = await forumRepository.GetSingleAsync(forumId);
        forum.Title = title;
        await forumRepository.UpdateAsync(forum);
        Console.WriteLine("Forum updated.");
    }

    public async Task DeleteAsync(int forumId)
    {
        await forumRepository.DeleteAsync(forumId);
        Console.WriteLine($"Forum deleted {forumId}");
    }
}