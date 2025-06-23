using Stripe;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using Stripe.Climate;

public class StripeService : IStripeService
{
    private readonly StripeSettings _stripeSettings;
    private readonly FrontendSettings _frontendSettings;
    private readonly ILogger<StripeService> _logger;
    private readonly IStripeUserAccountDetailsRepository _stripeUserAccountDetailsRepository;


    public StripeService(
        IOptions<StripeSettings> stripeSettings,
        ILogger<StripeService> logger,
        IOptions<FrontendSettings> frontendSettings,
        IStripeUserAccountDetailsRepository stripeUserAccountDetailsRepository
    )
    {
        _stripeSettings = stripeSettings.Value;
        _logger = logger;
        _frontendSettings = frontendSettings.Value;
        _stripeUserAccountDetailsRepository = stripeUserAccountDetailsRepository;
    }

    public async Task<AccountLink> CreateAccountLink(Account stripeAccount, AccountLinkType linkType)
    {
        return await CreateAccountLink(stripeAccount.Id, linkType);
    }

    public async Task<LoginLink> CreateAccountUpdateLink(StripeUserAccountDetails stripeAccount)
    {
        _logger.LogInformation("Creating new stripe link of type: {type}, for stripe account id: {id}",
            AccountLinkType.account_update,
            stripeAccount.stripeAccountId
        );

        var stripeClient = new Stripe.StripeClient(_stripeSettings.SecretKey);
        var service = new AccountLoginLinkService(stripeClient);

        LoginLink loginLink = service.Create(stripeAccount.stripeAccountId);

        stripeAccount.UpdateAccountLinkUrl = loginLink.Url;
        await _stripeUserAccountDetailsRepository.UpdateAsync(stripeAccount);

        return loginLink;
    }

    public async Task<AccountLink> CreateAccountLink(string stripeAccountId, AccountLinkType linkType)
    {
        _logger.LogInformation("Creating new account link of type:{linkType}; for stripeAccountId: {stripeAccountId}",
            linkType,
            stripeAccountId
        );
        var stripeClient = new Stripe.StripeClient(_stripeSettings.SecretKey);
        var accountLink = await new AccountLinkService(stripeClient).CreateAsync(new AccountLinkCreateOptions
        {
            Account = stripeAccountId,
            RefreshUrl = _frontendSettings.LinkToRefreshStripeAccountLink,
            ReturnUrl = _frontendSettings.LinkToUserProfile,
            Type = linkType.ToString(),
        });
        return accountLink;
    }

    public async Task<StripeUserAccountDetails> createStripeAccount(User user)
    {
        _logger.LogInformation("Creating a stripe account for user: {email}", user.Email);
        var options = new AccountCreateOptions
        {
            Country = "PL",
            Email = user.Email,
            Controller = new AccountControllerOptions
            {
                Fees = new AccountControllerFeesOptions { Payer = "application" },
                Losses = new AccountControllerLossesOptions { Payments = "application" },
                StripeDashboard = new AccountControllerStripeDashboardOptions
                {
                    Type = "express",
                },
            },
        };
        var stripeClient = new Stripe.StripeClient(_stripeSettings.SecretKey);
        var service = new AccountService(stripeClient);
        Account account = service.Create(options);

        var onboardingAccountLink = await CreateAccountLink(account, AccountLinkType.account_onboarding);

        var stripeUserAccountDetails = new StripeUserAccountDetails()
        {
            userId = user.Id,
            User = user,
            stripeAccountId = account.Id,
            OnboardingAccountLinkUrl = onboardingAccountLink.Url,
            OnboardingAccountLinkExpiration = onboardingAccountLink.ExpiresAt
        };

        await _stripeUserAccountDetailsRepository.SaveAsync(stripeUserAccountDetails);

        return stripeUserAccountDetails;
    }

    public async Task<StripeProductDetails> createStripeProduct(Product product)
    {
        _logger.LogInformation("Creating a StripeProduct for product with id: {productId}", product.Id);

        var productCreateOptions = new ProductCreateOptions()
        {
            Name = product.Name
        };

        var stripeClient = GetStripeClient();
        var stripeProductService = new Stripe.ProductService(stripeClient);

        Stripe.Product stripeProduct = stripeProductService.Create(productCreateOptions);
        Stripe.Price stripePrice = await createStripePriceForProduct(stripeProduct, product);

        return new StripeProductDetails()
        {
            StripeProductId = stripeProduct.Id,
            StripeProductPriceId = stripePrice.Id
        };
    }

    public async Task<Price> createStripePriceForProduct(Stripe.Product stripeProduct, Product product)
    {
        _logger.LogInformation("Creating a StripePrice for StripeProduct with id: {productId}", stripeProduct.Id);
        var stripeClient = GetStripeClient();

        var options = new PriceCreateOptions
        {
            Currency = "usd",
            UnitAmount = (long) (product.Price * 100),
            Product = stripeProduct.Id
        };
        var service = new PriceService(stripeClient);
        Price price = service.Create(options);

        return price;
    }

    public async Task<Stripe.Checkout.Session> CreateCheckoutSession(ShoppingCart shoppingCart, string userId)
    {
        _logger.LogInformation("Creating stripe checkout session for user: {userId}", userId);
        var stripeClient = GetStripeClient();

        var stripeLineItems = CreateSessionLineItemOptions(shoppingCart);
        var paymentIntentData = await CreateSessionPaymentIntentDataOptions(shoppingCart, userId);

        var options = new Stripe.Checkout.SessionCreateOptions
        {
            Mode = "payment",
            PaymentIntentData = paymentIntentData,
            LineItems = stripeLineItems,
            SuccessUrl = _frontendSettings.LinkToUserProfile,
            CancelUrl = _frontendSettings.LinkToUserProfile,
        };

        var service = new Stripe.Checkout.SessionService(stripeClient);

        Stripe.Checkout.Session session = service.Create(options);

        return session;
    }

    private List<Stripe.Checkout.SessionLineItemOptions> CreateSessionLineItemOptions(ShoppingCart shoppingCart)
    {
        _logger.LogInformation("Creating SessionLineItemOptions");
        var listOfLineItemOptios = new List<Stripe.Checkout.SessionLineItemOptions>();

        foreach (var product in shoppingCart.Products)
        {
            var sessionLineItemOptions = new Stripe.Checkout.SessionLineItemOptions
            {
                Price = product.StripeProductPriceId,
                Quantity = product.Amount
            };

            listOfLineItemOptios.Add(sessionLineItemOptions);
        }
        return listOfLineItemOptios;
    }

    private async Task<Stripe.Checkout.SessionPaymentIntentDataOptions> CreateSessionPaymentIntentDataOptions(ShoppingCart shoppingCart, string userId) {
        _logger.LogInformation("Creating SessionPaymentIntentDataOptions");
        var stripeUserAccountDetails = await _stripeUserAccountDetailsRepository.FindByUserId(userId);
        decimal totalPrice = shoppingCart.Products.Sum(p => p.Price);
        int applicationFee = (int)((decimal)0.02 * totalPrice);

        return new Stripe.Checkout.SessionPaymentIntentDataOptions()
        {
            ApplicationFeeAmount = applicationFee * 100,
            TransferData = new Stripe.Checkout.SessionPaymentIntentDataTransferDataOptions
            {
                Destination = stripeUserAccountDetails.stripeAccountId
            }
        };
    }

    private Stripe.StripeClient GetStripeClient()
    {
        return new StripeClient(_stripeSettings.SecretKey);
    }
}