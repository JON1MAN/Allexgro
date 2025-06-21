
public interface IStripeUserAccountDetailsRepository
{
    public Task SaveAsync(StripeUserAccountDetails stripeUserAccountDetails);
}