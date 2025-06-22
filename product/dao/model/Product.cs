public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int ProductCategoryId { get; set; }
    public int ProductTypeId { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public ProductType ProductType { get; set; }
    public ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
    public string UserId { get; set; }
    public User User { get; set; } 
    public string? StripeProductId { get; set; }
    public string? StripeProductPriceId { get; set; }
    public Product() { }

    public Product(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
