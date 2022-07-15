using Microsoft.AspNetCore.Mvc;
using SpendingTrackerAPI.DTOModels;
using SpendingTrackerAPI.Services;

namespace SpendingTrackerAPI.Controllers;


[Route("api/users")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;


    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody]CreateUserDTO createUserDto)
    {
        _userService.Register(createUserDto);
        return Ok();
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody]LoginUserDTO loginUserDto)
    {
        string token = _userService.Login(loginUserDto);
        return Ok(token);
    }
}