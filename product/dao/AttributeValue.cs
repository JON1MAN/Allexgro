
public class AttributeValue {
    public int Id {get; set;}

    //TODO many to one
    public AttributeKey attributeKey {get; set;}

    public string StringValue {get; set;}
    public decimal DecimalValue {get; set;}
    public bool BooleanValue {get; set;}
}
