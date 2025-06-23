
using System.Threading.Tasks;

public class CheckoutService : ICheckoutService
{

    private readonly ILogger<CheckoutService> _logger;
    private readonly IStripeService _stripeService;
    private readonly IProductService _productService;

    public CheckoutService(ILogger<CheckoutService> logger, IStripeService stripeService, IProductService productService)
    {
        _stripeService = stripeService;
        _logger = logger;
        _productService = productService;
    }

    public async Task<string> PerformPayment(ShoppingCart shoppingCart, string userId)
    {
        _logger.LogInformation("Performing a payment by user id: {userId}", userId);
        var stripeCheckoutSession = await _stripeService.CreateCheckoutSession(shoppingCart, userId);

        // if (stripeCheckoutSession != null)
        // {
        //     _productService.UpdateProductAmounts(shoppingCart);
        // }

        string checkoutUrl = stripeCheckoutSession.Url;

        return checkoutUrl;
    }
}