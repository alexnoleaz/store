using Store.Products.Dtos;
using Store.Shared;
using Store.Shared.Dependency;
using Store.Shared.Dtos;
using Store.Shared.Repositories;
using Store.Shared.Validations;

namespace Store.Products;

public class ProductService : IProductService, ITransientDependency
{
    private readonly IRepository<Product> _repository;
    private readonly IObjectMapper _objectMapper;

    public ProductService(IRepository<Product> repository, IObjectMapper objectMapper)
    {
        ArgumentValidator.NotNull(repository);
        ArgumentValidator.NotNull(objectMapper);

        _repository = repository;
        _objectMapper = objectMapper;
    }

    public async Task<ProductDto> Create(CreateProductDto input)
    {
        ArgumentValidator.NotNull(input);

        var product = _objectMapper.Map<Product>(input);
        var dbProduct = await _repository.InsertAsync(product);
        await _repository.SaveChangesAsync();

        return _objectMapper.Map<ProductDto>(dbProduct);
    }

    public async Task<bool> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        var result = await _repository.SaveChangesAsync();

        return result > 0;
    }

    public async Task<ProductDto?> Get(int id)
    {
        var product = await _repository.GetAsync(id);
        return product is null ? null : _objectMapper.Map<ProductDto>(product);
    }

    public async Task<PagedResult<ProductDto>> GetAll(ProductQueryDto input)
    {
        ArgumentValidator.NotNull(input);

        var totalCount = await _repository.CountAsync();
        var query = await _repository.GetAllAsync();
        var items = query.Skip((input.Page - 1) * input.PageSize).Take(input.PageSize).ToList();

        return new PagedResult<ProductDto>
        {
            Items = _objectMapper.Map<IEnumerable<ProductDto>>(items),
            TotalCount = totalCount,
            Page = input.Page,
            PageSize = input.PageSize,
        };
    }

    public async Task<ProductDto?> Update(UpdateProductDto input)
    {
        ArgumentValidator.NotNull(input);

        var dbProduct = await _repository.UpdateAsync(_objectMapper.Map<Product>(input));
        var result = await _repository.SaveChangesAsync();
        return result > 0 ? _objectMapper.Map<ProductDto>(dbProduct) : null;
    }
}
