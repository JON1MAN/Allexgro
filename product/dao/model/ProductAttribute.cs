
using System.Reflection.Metadata.Ecma335;

public class ProductAttribute
{
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
    public int AttributeKeyId { get; set; }
    public ProductAttributeKey AttributeKey { get; set; }
    public bool? BooleanValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public string StringValue { get; set; }

}
