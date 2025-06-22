using System.Threading.Tasks;
using AutoMapper;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _userMapper;
    private readonly IStripeService _stripeService;

    public UserService(ILogger<UserService> logger, IUserRepository userRepository, IMapper userMapper, IStripeService stripeService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _userMapper = userMapper;
        _stripeService = stripeService;
    }
    public async Task<UserDTO?> getCurrentLoggedUserProfileData(string userId)
    {
        _logger.LogInformation("Fetching profile data for user: {userId}", userId);
        var user = _userRepository.FindCurrentLoggedUserProfileData(userId);
        var editUserDTO = _userMapper.Map<UserDTO>(user);
        if (user.stripeUserAccountDetails.UpdateAccountLinkUrl == null || user.stripeUserAccountDetails.UpdateAccountLinkExpiration < DateTime.Now)
        {
            var link = await _stripeService.CreateAccountUpdateLink(user.stripeUserAccountDetails);
            editUserDTO.UpdateStripeAccountLinkUrl = link.Url;
        } else {
            editUserDTO.UpdateStripeAccountLinkUrl = user.stripeUserAccountDetails.UpdateAccountLinkUrl;
        }
        
        return editUserDTO;
    }

    public async Task<User> updateCurrentLoggedUserProfileData(UserEditDTO userEditDTO, string userId)
    {
        _logger.LogInformation("Updating user with id: {userId}", userId);
        var user = _userRepository.FindCurrentLoggedUserProfileData(userId);

        user.Email = userEditDTO.Email;
        user.Description = userEditDTO.Description;

        var updatedUser = await _userRepository.UpdateUser(user);

        return updatedUser;
    }
}