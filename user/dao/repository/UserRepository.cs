

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public User? FindCurrentLoggedUserProfileData(string userId)
    {
        return _context.Users.FirstOrDefault(user => user.Id == userId);
    }
}