
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ISecurityUtils _securityUtils;
    private readonly IMapper _userMapper;

    public UserController(IUserService userService, ISecurityUtils securityUtils, IMapper userMapper)
    {
        _userService = userService;
        _securityUtils = securityUtils;
        _userMapper = userMapper;
    }

    [HttpGet("me")]
    [ProducesResponseType(200)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult<UserDTO> FindById([FromRoute] int id)
    {
        var userId = _securityUtils.getCurrentLoggedUserId();
        var response = _userService.getCurrentLoggedUserProfileData(userId);

        if (response == null)
            return NotFound();

        return Ok(_userMapper.Map<UserDTO>(response));
    }
}