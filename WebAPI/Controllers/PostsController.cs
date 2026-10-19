using DTOs;
using DTOs.Post;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository postRepo;
    private readonly IUserRepository userRepo;
    private readonly IForumRepository forumRepo;

    public PostsController(IPostRepository postRepo,
                           IUserRepository userRepo,
                           IForumRepository forumRepo)
    {
        this.postRepo = postRepo;
        this.userRepo = userRepo;
        this.forumRepo = forumRepo;
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> AddPost([FromBody] CreatePostDto request)
    {
        try
        {
            User user = await userRepo.GetSingleAsync(request.UserId);
            await forumRepo.GetSingleAsync(request.ForumId); 
            Post post = new(request.UserId, request.Title, request.Body)
            {
                ForumId = request.ForumId
            };
            Post created = await postRepo.AddAsync(post);

            PostDto dto = ToDto(created, user.Username);
            return Created($"/Posts/{dto.Id}", dto);
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
    public async Task<ActionResult<PostDto>> GetPostById([FromRoute] int id)
    {
        try
        {
            Post post = await postRepo.GetSingleAsync(id);
            return Ok(ToDto(post, UserNameOf(post.UserId)));
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
    public ActionResult<IEnumerable<PostDto>> GetPosts([FromQuery] int? forumId,
                                                       [FromQuery] int? userId,
                                                       [FromQuery] string? title)
    {
        try
        {
            IQueryable<Post> posts = postRepo.GetMany();

            if (forumId is not null)
                posts = posts.Where(p => p.ForumId == forumId);

            if (userId is not null)
                posts = posts.Where(p => p.UserId == userId);

            if (!string.IsNullOrWhiteSpace(title))
                posts = posts.Where(p => p.Title.ToLower().Contains(title.ToLower()));

            Dictionary<int, string> userNames = userRepo.GetMany()
                .ToDictionary(u => u.Id, u => u.Username);

            List<PostDto> dtos = posts
                .Select(p => ToDto(p, userNames.GetValueOrDefault(p.UserId, "Unknown")))
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
    public async Task<ActionResult> UpdatePost([FromRoute] int id, [FromBody] UpdatePostDto request)
    {
        try
        {
            Post existing = await postRepo.GetSingleAsync(id);
            existing.Title = request.Title;
            existing.Body = request.Body;
            await postRepo.UpdateAsync(existing);
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
    public async Task<ActionResult> DeletePost([FromRoute] int id)
    {
        try
        {
            await postRepo.DeleteAsync(id);
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

    private static PostDto ToDto(Post post, string userName) => new()
    {
        Id = post.Id,
        UserId = post.UserId,
        UserName = userName,
        ForumId = post.ForumId,
        Title = post.Title,
        Body = post.Body
    };
}