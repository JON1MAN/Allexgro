using Stripe;
public interface IStripeService
{
    public Task<AccountLink> CreateAccountLink(Account stripeAccount, AccountLinkType linkType);
    public Task<AccountLink> CreateAccountLink(string stripeAccountId, AccountLinkType linkType);
    public Task<StripeUserAccountDetails> createStripeAccount(User user);
    public Task<LoginLink> CreateAccountUpdateLink(StripeUserAccountDetails stripeAccount);
    public Task<StripeProductDetails> createStripeProduct(Product product);
    public Task<Price> createStripePriceForProduct(Stripe.Product stripeProduct, Product product);
    public Task<Stripe.Checkout.Session> CreateCheckoutSession(ShoppingCart shoppingCart, string userId);
}