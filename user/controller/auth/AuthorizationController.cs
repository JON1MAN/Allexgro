using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthorizationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<ActionResult<Response<TokenResponseDTO>>> RegisterUser([FromBody] UserLoginDTO request)
    {
        var response = await _authService.LoginUser(request);
        return Ok(response);
    }
}