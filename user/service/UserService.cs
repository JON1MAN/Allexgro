
public class UserService : IUserService
{
    private readonly ISecurityUtils _securityUtils;
    private readonly ILogger<UserService> _logger;

    public UserService(ISecurityUtils securityUtils, ILogger<UserService> logger)
    {
        _securityUtils = securityUtils;
        _logger = logger;
    }
    public string? getCurrentLoggedUserId()
    {
        return _securityUtils.getCurrentLoggedUserId();
    }
}