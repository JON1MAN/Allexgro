
public interface IUserService
{
    public Task<UserDTO> getCurrentLoggedUserProfileData(string userId);
    public Task<User> updateCurrentLoggedUserProfileData(UserEditDTO userEditDTO, string userId);
}