
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<Product> SellerProducts { get; set; } = new List<Product>();
}