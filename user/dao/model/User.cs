
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<Product> SellerProducts { get; set; } = new List<Product>();
    public string? Description { get; set; }
    public StripeUserAccountDetails stripeUserAccountDetails { get; set; }
}