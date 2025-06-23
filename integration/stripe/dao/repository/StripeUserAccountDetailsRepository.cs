
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class StripeUserAccountDetailsRepository : IStripeUserAccountDetailsRepository
{
    private readonly DataContext _context;

    public StripeUserAccountDetailsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(StripeUserAccountDetails stripeUserAccountDetails)
    {
        _context.StripeUserAccountDetails.Add(stripeUserAccountDetails);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(StripeUserAccountDetails stripeUserAccountDetails)
    {
        _context.StripeUserAccountDetails.Update(stripeUserAccountDetails);
        await _context.SaveChangesAsync();
    }

    public async Task<StripeUserAccountDetails> FindByUserId(string userId)
    {
        var result = await _context.StripeUserAccountDetails.FirstOrDefaultAsync(details => details.userId == userId);
        return result;
    }
}