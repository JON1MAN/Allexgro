
public class ProductType
{
    public int Id { get; set; }
    public String Name { get; set; }
    public int ProductCategoryId { get; set; }
    public ProductCategory ProductCategory { get; set; }

    public ICollection<ProductAttributeKey> AttributeKeys { get; set; } = new List<ProductAttributeKey>();
}
