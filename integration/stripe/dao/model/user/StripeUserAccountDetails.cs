
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class StripeUserAccountDetails
{
    [Key, ForeignKey("User")]
    public string userId { get; set; }
    public User User { get; set; }
    public string stripeAccountId { get; set; }
    public string OnboardingAccountLinkUrl { get; set; }
    public DateTime OnboardingAccountLinkExpiration { get; set; }
    public string? UpdateAccountLinkUrl { get; set; }
    public DateTime? UpdateAccountLinkExpiration { get; set; }
}
