using Stripe;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading.Tasks;

public class StripeService : IStripeService
{
    private readonly StripeSettings _stripeSettings;
    private readonly FrontendSettings _frontendSettings;
    private readonly ILogger<UserService> _logger;
    private readonly IStripeUserAccountDetailsRepository _stripeUserAccountDetailsRepository;


    public StripeService(
        IOptions<StripeSettings> stripeSettings,
        ILogger<UserService> logger,
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

        var accountLink = await CreateAccountLink(account, AccountLinkType.account_onboarding);

        var stripeUserAccountDetails = new StripeUserAccountDetails()
        {
            userId = user.Id,
            User = user,
            stripeAccountId = account.Id,
            OnboardingAccountLinkUrl = accountLink.Url,
            OnboardingAccountLinkExpiration = accountLink.ExpiresAt
        };

        await _stripeUserAccountDetailsRepository.SaveAsync(stripeUserAccountDetails);

        return stripeUserAccountDetails;
    }
}