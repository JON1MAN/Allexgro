
public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository _userRepository;

    public UserService(ILogger<UserService> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }
    public User? getCurrentLoggedUserProfileData(string userId)
    {
        _logger.LogInformation("Fetching profile data for user: {userId}", userId);
        return _userRepository.FindCurrentLoggedUserProfileData(userId);
    }
}