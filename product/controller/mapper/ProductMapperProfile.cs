
using AutoMapper;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<Product, ProductCreateDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<ProductAttribute, ProductAttributeDTO>().ReverseMap();
    }
}