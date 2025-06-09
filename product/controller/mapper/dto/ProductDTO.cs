
public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public ProductCategory ProductCategory { get; set; } = null!;
    public ProductType ProductType { get; set; } = null!;
    public ICollection<ProductAttributeDTO> ProductAttributes { get; set; } = new List<ProductAttributeDTO>();

}