using AniMangaMVCCoreApp.Dtos.User;
using AniMangaMVCCoreApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AniMangaMVCCoreApp.Controllers;

[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    
    public UserController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCreateRequestDto userCreateRequestDto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Create the user given the account details provided.
            var user = new User
            {
                UserName = userCreateRequestDto.UserName,
                Email = userCreateRequestDto.Email
            };
            
            var userResult = await _userManager.CreateAsync(user, userCreateRequestDto.Password);

            if (!userResult.Succeeded) return BadRequest(userResult.Errors);
            
            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (roleResult.Succeeded) return Ok("User created");

            return BadRequest(roleResult.Errors);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}