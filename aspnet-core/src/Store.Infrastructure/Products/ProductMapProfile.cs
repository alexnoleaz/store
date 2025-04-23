using AutoMapper;
using Store.Products.Dtos;

namespace Store.Products;

public class ProductMapProfile : Profile
{
    public ProductMapProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}