using Stripe;
public interface IStripeService
{
    public Task<AccountLink> CreateAccountLink(Account stripeAccount, AccountLinkType linkType);
    public Task<AccountLink> CreateAccountLink(string stripeAccountId, AccountLinkType linkType);
    public Task<StripeUserAccountDetails> createStripeAccount(User user);

}