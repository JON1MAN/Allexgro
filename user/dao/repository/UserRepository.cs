

using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public User? FindCurrentLoggedUserProfileData(string userId)
    {
        return _context.Users
            .Include(u => u.stripeUserAccountDetails)
            .FirstOrDefault(user => user.Id == userId);
    }

    public async Task<User>? UpdateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}