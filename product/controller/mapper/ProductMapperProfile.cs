
using AutoMapper;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<Product, ProductCreateDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<ProductAttribute, ProductAttributeDTO>().ReverseMap();
        CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
        CreateMap<ProductType, ProductTypeDTO>().ReverseMap();
        CreateMap<ProductAttributeKey, ProductAttributeKeyDTO>().ReverseMap();
    }
}