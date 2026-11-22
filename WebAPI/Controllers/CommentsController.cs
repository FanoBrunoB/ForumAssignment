using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepo;
    private readonly IUserRepository userRepo;
    private readonly IPostRepository postRepo;

    public CommentsController(ICommentRepository commentRepo,
                              IUserRepository userRepo,
                              IPostRepository postRepo)
    {
        this.commentRepo = commentRepo;
        this.userRepo = userRepo;
        this.postRepo = postRepo;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> AddComment([FromBody] CreateCommentDto request)
    {
        try
        {
            User user = await userRepo.GetSingleAsync(request.UserId);
            await postRepo.GetSingleAsync(request.PostId); 

            Comment comment = new(request.UserId, request.PostId, request.Body);
            Comment created = await commentRepo.AddAsync(comment);

            CommentDto dto = ToDto(created, user.Username);
            return Created($"/Comments/{dto.Id}", dto);
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
    public async Task<ActionResult<CommentDto>> GetCommentById([FromRoute] int id)
    {
        try
        {
            Comment comment = await commentRepo.GetSingleAsync(id);
            return Ok(ToDto(comment, UserNameOf(comment.UserId)));
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
    public ActionResult<IEnumerable<CommentDto>> GetAllComments([FromQuery] int? userId,
                                                                [FromQuery] int? postId)
    {
        try
        {
            IQueryable<Comment> comments = commentRepo.GetMany();

            if (postId is not null)
                comments = comments.Where(c => c.PostId == postId);

            if (userId is not null)
                comments = comments.Where(c => c.UserId == userId);

            Dictionary<int, string> userNames = userRepo.GetMany()
                .ToDictionary(u => u.Id, u => u.Username);

            List<CommentDto> dtos = comments
                .Select(c => ToDto(c, userNames.GetValueOrDefault(c.UserId, "Unknown")))
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
    public async Task<ActionResult> UpdateComment([FromRoute] int id,
                                                  [FromBody] UpdateCommentDto request)
    {
        try
        {
            Comment existing = await commentRepo.GetSingleAsync(id);
            existing.Body = request.Body;
            await commentRepo.UpdateAsync(existing);
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
    public async Task<ActionResult> DeleteComment([FromRoute] int id)
    {
        try
        {
            await commentRepo.DeleteAsync(id);
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

    private static CommentDto ToDto(Comment comment, string userName) => new()
    {
        Id = comment.Id,
        UserId = comment.UserId,
        PostId = comment.PostId,
        Body = comment.Body,
        UserName = userName
    };
}