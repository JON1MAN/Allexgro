
public class ProductCreateDTO
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int ProductCategoryId { get; set; }
    public int ProductTypeId { get; set; }
    public ICollection<ProductAttributeDTO> ProductAttributes { get; set; }
}