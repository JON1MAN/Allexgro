
public interface IUserRepository
{
    public User? FindCurrentLoggedUserProfileData(string userId);
}