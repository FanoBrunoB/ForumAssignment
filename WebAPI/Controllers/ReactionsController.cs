using DTOs;
using DTOs.Reaction;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReactionsController : ControllerBase
{
    private readonly IReactionRepository reactionRepo;
    private readonly IUserRepository userRepo;
    private readonly IPostRepository postRepo;
    private readonly ICommentRepository commentRepo;

    public ReactionsController(IReactionRepository reactionRepo,
                               IUserRepository userRepo,
                               IPostRepository postRepo,
                               ICommentRepository commentRepo)
    {
        this.reactionRepo = reactionRepo;
        this.userRepo = userRepo;
        this.postRepo = postRepo;
        this.commentRepo = commentRepo;
    }

    [HttpPost]
    public async Task<ActionResult<ReactionDto>> AddReaction([FromBody] CreateReactionDto request)
    {
        if ((request.PostId is null) == (request.CommentId is null))
            return BadRequest("Set exactly one of PostId or CommentId");

        try
        {
            await userRepo.GetSingleAsync(request.UserId);

            if (request.PostId is not null)
                await postRepo.GetSingleAsync(request.PostId.Value);
            else
                await commentRepo.GetSingleAsync(request.CommentId!.Value);
            bool alreadyReacted = reactionRepo.GetMany().Any(r =>
                r.UserId == request.UserId &&
                r.PostId == request.PostId &&
                r.CommentId == request.CommentId);
            if (alreadyReacted)
                return Conflict("User has already reacted to this item");

            Reaction reaction = new(request.UserId, request.Type)
            {
                PostId = request.PostId,
                CommentId = request.CommentId
            };
            Reaction created = await reactionRepo.AddAsync(reaction);

            ReactionDto dto = ToDto(created);
            return Created($"/Reactions/{dto.Id}", dto);
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
    public async Task<ActionResult<ReactionDto>> GetReactionById([FromRoute] int id)
    {
        try
        {
            Reaction reaction = await reactionRepo.GetSingleAsync(id);
            return Ok(ToDto(reaction));
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
    public ActionResult<IEnumerable<ReactionDto>> GetReactions([FromQuery] int? postId,
                                                               [FromQuery] int? commentId,
                                                               [FromQuery] int? userId,
                                                               [FromQuery] ReactionType? type)
    {
        try
        {
            IQueryable<Reaction> reactions = reactionRepo.GetMany();

            if (postId is not null)
                reactions = reactions.Where(r => r.PostId == postId);

            if (commentId is not null)
                reactions = reactions.Where(r => r.CommentId == commentId);

            if (userId is not null)
                reactions = reactions.Where(r => r.UserId == userId);

            if (type is not null)
                reactions = reactions.Where(r => r.Type == type);

            return Ok(reactions.Select(r => ToDto(r)).ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateReaction([FromRoute] int id, [FromBody] UpdateReactionDto request)
    {
        try
        {
            Reaction existing = await reactionRepo.GetSingleAsync(id);
            existing.Type = request.Type;
            await reactionRepo.UpdateAsync(existing);
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
    public async Task<ActionResult> DeleteReaction([FromRoute] int id)
    {
        try
        {
            await reactionRepo.DeleteAsync(id);
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

    private static ReactionDto ToDto(Reaction reaction) => new()
    {
        Id = reaction.Id,
        UserId = reaction.UserId,
        Type = reaction.Type,
        PostId = reaction.PostId,
        CommentId = reaction.CommentId
    };
}