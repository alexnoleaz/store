using Store.Products.Dtos;
using Store.Shared.Dtos;

namespace Store.Products;

public interface IProductService
{
    Task<ProductDto?> Get(int id);
    Task<PagedResult<ProductDto>> GetAll(ProductQueryDto input);
    Task<ProductDto> Create(CreateProductDto input);
    Task<ProductDto?> Update(UpdateProductDto input);
    Task<bool> Delete(int id);
}