
public interface ICheckoutService
{
    public Task<string> PerformPayment(ShoppingCart shoppingCart, string userId);
}