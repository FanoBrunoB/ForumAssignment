using RepositoryContracts;
using Entities;
namespace CLI.UI.ManageForums;

public class ForumsListView(IForumRepository forumRepository)
{
    public void GetMany()
    {
        IQueryable<Forum> forums = forumRepository.GetMany();

        Console.WriteLine("=== All Forums ===");
        foreach (Forum forum in forums)
        {
            Console.WriteLine($"[{forum.Id}] {forum.Title}");
        }
    }
}