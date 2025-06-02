
using System.Reflection.Metadata.Ecma335;

public class ProductAttribute
{
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
    public ICollection<ProductAttributeKey> AttributeKeys { get; set; } = new List<ProductAttributeKey>();
    public bool? BooleanValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public string StringValue { get; set; }

}
