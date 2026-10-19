using DTOs;
using DTOs.Forum;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ForumsController : ControllerBase
{
    private readonly IForumRepository forumRepo;
    private readonly IUserRepository userRepo;

    public ForumsController(IForumRepository forumRepo, IUserRepository userRepo)
    {
        this.forumRepo = forumRepo;
        this.userRepo = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<ForumDto>> AddForum([FromBody] CreateForumDto request)
    {
        try
        {
            User user = await userRepo.GetSingleAsync(request.UserId);

            Forum created = await forumRepo.AddAsync(new Forum(request.UserId, request.Title));
            ForumDto dto = ToDto(created, user.Username);
            return Created($"/Forums/{dto.Id}", dto);
        }
        catch (InvalidOperationException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ForumDto>> GetForumById([FromRoute] int id)
    {
        try
        {
            Forum forum = await forumRepo.GetSingleAsync(id);
            return Ok(ToDto(forum, UserNameOf(forum.UserId)));
        }
        catch (InvalidOperationException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    public ActionResult<IEnumerable<ForumDto>> GetForums([FromQuery] string? title,
                                                         [FromQuery] int? userId)
    {
        try
        {
            IQueryable<Forum> forums = forumRepo.GetMany();

            if (!string.IsNullOrWhiteSpace(title))
                forums = forums.Where(f => f.Title.ToLower().Contains(title.ToLower()));

            if (userId is not null)
                forums = forums.Where(f => f.UserId == userId);

            Dictionary<int, string> userNames = userRepo.GetMany()
                .ToDictionary(u => u.Id, u => u.Username);

            List<ForumDto> dtos = forums
                .Select(f => ToDto(f, userNames.GetValueOrDefault(f.UserId, "unknown")))
                .ToList();

            return Ok(dtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateForum([FromRoute] int id, [FromBody] UpdateForumDto request)
    {
        try
        {
            Forum existing = await forumRepo.GetSingleAsync(id);
            existing.Title = request.Title;
            await forumRepo.UpdateAsync(existing);
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteForum([FromRoute] int id)
    {
        try
        {
            await forumRepo.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    private string UserNameOf(int userId) =>
        userRepo.GetMany().FirstOrDefault(u => u.Id == userId)?.Username ?? "Unknown";

    private static ForumDto ToDto(Forum forum, string userName) => new()
    {
        Id = forum.Id,
        UserId = forum.UserId,
        UserName = userName,
        Title = forum.Title
    };
}