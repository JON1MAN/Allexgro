
using System.Security.Claims;

public class SecurityUtils : ISecurityUtils
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    public SecurityUtils(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? getCurrentLoggedUserId()
    {
        return _httpContextAccessor.HttpContext
                    ?.User.FindFirst(ClaimTypes.NameIdentifier)
                    ?.Value;
    }
}