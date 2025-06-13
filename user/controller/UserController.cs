
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("me")]
    [ProducesResponseType(200)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult<string> FindById([FromRoute] int id)
    {
        var response = _userService.getCurrentLoggedUserId();
        if (response == null)
            return NotFound();

        return Ok(response);
    }
}