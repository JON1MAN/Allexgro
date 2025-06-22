
public interface IUserRepository
{
    public User? FindCurrentLoggedUserProfileData(string userId);
    public Task<User>? UpdateUser(User user);
}