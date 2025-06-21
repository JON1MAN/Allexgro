
using System.Threading.Tasks;

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
}