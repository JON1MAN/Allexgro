
public class UserService : IUserService
{
    private readonly ISecurityUtils _securityUtils;

    public UserService(ISecurityUtils securityUtils)
    {
        _securityUtils = securityUtils;
    }
    public string? getCurrentLoggedUserId()
    {
        return _securityUtils.getCurrentLoggedUserId();
    }
}