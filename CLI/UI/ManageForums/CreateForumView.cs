using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageForums;

public class CreateForumView(IForumRepository forumRepository)
{
    public async Task CreateAsync()
    {
        Console.Write("Title: ");
        string title = Console.ReadLine()!;
        string userId = Console.ReadLine()!;

        Forum forum = new Forum(int.Parse(userId) , title);

        Forum createdForum = await forumRepository.AddAsync(forum);

        Console.WriteLine($"Forum created: {createdForum.Title} (id: {createdForum.Id})");
    }
}