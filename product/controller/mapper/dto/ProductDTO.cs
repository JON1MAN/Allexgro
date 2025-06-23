
public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public string UserId { get; set; }
    public string? StripeProductId { get; set; }
    public string? StripeProductPriceId { get; set; }
    public ProductCategoryDTO? ProductCategory { get; set; } = null!;
    public ProductTypeDTO? ProductType { get; set; } = null!;
    public ICollection<ProductAttributeDTO>? ProductAttributes { get; set; } = new List<ProductAttributeDTO>();
}