using DTOs.Auth;
using DTOs.User;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    
    private readonly IUserRepository userRepo;
    public AuthController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            User? user = userRepo.GetMany()
                .FirstOrDefault(u => u.Username.ToLower() == request.UserName.ToLower());

            if (user != null && user.Password == request.Password)
            {
                UserDto dto =  new UserDto { Id = user.Id, UserName = user.Username };
                return Ok(dto);
            }
            else
            {
                return Unauthorized();
            }
            
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
    
}