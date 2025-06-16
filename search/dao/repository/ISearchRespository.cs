
public interface ISearchRepository
{
    public ICollection<Product> filterProducts(FilterDTO filterDTO);
}