using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

[ApiController]
[Route("api/v1/[controller]")]
public class RegistrationController : ControllerBase
{

    private readonly IAuthService _authService;

    public RegistrationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<ActionResult<Response<TokenResponseDTO>>> RegisterUser([FromBody] UserRegistrationDTO request)
    {
        Console.WriteLine($"DTO Email: {request.Email}, Password: {request.Password}");
        var response = await _authService.RegisterUser(request);
        return Ok(response);
    }

}