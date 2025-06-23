
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

[ApiController]
[Route("api/v1/[controller]")]
public class CheckoutController : ControllerBase
{

    private readonly ICheckoutService _checkoutService;
    private readonly ISecurityUtils _securityUtils;

    public CheckoutController(ICheckoutService checkoutService, ISecurityUtils securityUtils)
    {
        _checkoutService = checkoutService;
        _securityUtils = securityUtils;
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(200)]
    public async Task<ActionResult<CheckoutSessionResponseUrlDTO>> CreateStripeCheckoutSession([FromBody] ShoppingCart request)
    {
        var userId = _securityUtils.getCurrentLoggedUserId();
        var checkoutSessionUrl = await _checkoutService.PerformPayment(request, userId);

        return Ok(new CheckoutSessionResponseUrlDTO(){CheckoutSessionResponseUrl = checkoutSessionUrl});
    }

}