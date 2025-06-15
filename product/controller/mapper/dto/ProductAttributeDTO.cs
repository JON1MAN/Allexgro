
public class ProductAttributeDTO
{
    public int ProductTypeId { get; set; }
    public int AttributeKeyId { get; set; }
    public ProductAttributeKeyDTO? AttributeKey { get; set; }
    public bool? BooleanValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public string StringValue { get; set; }
}