
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.Extensions.Options;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ISecurityUtils _securityUtils;
    private readonly IMapper _userMapper;
    private readonly StripeSettings _stripe;

    public UserController(IUserService userService, ISecurityUtils securityUtils, IMapper userMapper, IOptions<StripeSettings> stripeSettings)
    {
        _userService = userService;
        _securityUtils = securityUtils;
        _userMapper = userMapper;
        _stripe = stripeSettings.Value;
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

    [HttpGet("key")]
    [ProducesResponseType(200)]
    public ActionResult<string> GetKey()
    {
        string key = _stripe.SecretKey;
        return Ok(key);
    }
}