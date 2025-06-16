
public class SearchRepository : ISearchRepository
{
    private readonly DataContext _context;

    public SearchRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<Product> filterProducts(FilterDTO filterDTO)
    {
        List<Product> result = new List<Product>();
        IQueryable<Product> query = _context.Products.AsQueryable();

        if (filterDTO.minPrice != null)
        {
            query = query.Where(p => p.Price >= filterDTO.minPrice);
        }

        if (filterDTO.maxPrice != null)
        {
            query = query.Where(p => p.Price <= filterDTO.maxPrice);
        }

        if (filterDTO.ProductCategoryId != null)
        {
            query = query.Where(p => p.ProductCategoryId == filterDTO.ProductCategoryId);
        }

        if (filterDTO.ProductTypeId != null)
        {
            query = query.Where(p => p.ProductTypeId == filterDTO.ProductTypeId);
        }

        if (filterDTO.ProductAttributes != null)
        {
            foreach (var attribute in filterDTO.ProductAttributes)
            {
                query = query.Where(
                    p => p.ProductAttributes.Any(pAttr =>
                        pAttr.AttributeKeyId == attribute.AttributeKeyId && 
                        (
                            attribute.AttributeKey.DataType == "STRING" && pAttr.StringValue.Equals(attribute.StringValue) ||
                            attribute.AttributeKey.DataType == "DECIMAL" && pAttr.DecimalValue == attribute.DecimalValue ||
                            attribute.AttributeKey.DataType == "BOOLEAN" && pAttr.BooleanValue == attribute.BooleanValue
                        )
                    )
                );
            }
        }

        return query
                    .OrderBy(p => p.Price)
                    .ToList();
    }
}