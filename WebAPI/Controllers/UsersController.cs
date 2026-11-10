using DTOs;
using DTOs.User;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
    {
        try
        {
            await VerifyUserNameIsAvailableAsync(request.UserName);

            User created = await userRepo.AddAsync(new User(request.UserName, request.Password));
            UserDto dto = ToDto(created);
            return Created($"/Users/{dto.Id}", dto);
        }
        catch (ArgumentException e)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetUserById([FromRoute] int id)
    {
        try
        {
            User user = await userRepo.GetSingleAsync(id);
            return Ok(ToDto(user));
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
    public ActionResult<IEnumerable<UserDto>> GetUsers([FromQuery] string? userName)
    {
        try
        {
            IQueryable<User> users = userRepo.GetMany();

            if (!string.IsNullOrWhiteSpace(userName))
                users = users.Where(u => u.Username.ToLower().Contains(userName.ToLower()));

            return Ok(users.Select(u => ToDto(u)).ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDto request)
    {
        try
        {
            User existing = await userRepo.GetSingleAsync(id);
            if (!existing.Username.Equals(request.UserName, StringComparison.OrdinalIgnoreCase))
                await VerifyUserNameIsAvailableAsync(request.UserName);

            existing.Username = request.UserName;
            existing.Password = request.Password;
            await userRepo.UpdateAsync(existing);
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        try
        {
            await userRepo.DeleteAsync(id);
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

    private Task VerifyUserNameIsAvailableAsync(string userName)
    {
        bool taken = userRepo.GetMany()
            .Any(u => u.Username.ToLower() == userName.ToLower());

        if (taken)
            throw new ArgumentException($"Username '{userName}' is already taken");

        return Task.CompletedTask;
    }

    private static UserDto ToDto(User user) => new()
    {
        Id = user.Id,
        UserName = user.Username,
        // we need to create a login endpoint because this is not correct
        Password = user.Password
    };
}