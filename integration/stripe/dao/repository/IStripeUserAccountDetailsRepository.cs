
public interface IStripeUserAccountDetailsRepository
{
    public Task SaveAsync(StripeUserAccountDetails stripeUserAccountDetails);
    public Task UpdateAsync(StripeUserAccountDetails stripeUserAccountDetails);
}