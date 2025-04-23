using Microsoft.AspNetCore.Mvc;
using Store.Products.Dtos;
using Store.Shared.Dtos;
using Store.Shared.Validations;

namespace Store.Products;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService productService)
    {
        ArgumentValidator.NotNull(productService);
        _service = productService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ProductDto>>> GetAll(
        [FromQuery] ProductQueryDto input
    )
    {
        ArgumentValidator.NotNull(input);

        var result = await _service.GetAll(input);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> Get(int id)
    {
        var result = await _service.Get(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto input)
    {
        ArgumentValidator.NotNull(input);

        var result = await _service.Create(input);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> Update(int id, [FromBody] UpdateProductDto input)
    {
        ArgumentValidator.NotNull(input);

        if (id != input.Id)
            return BadRequest("ID mismatch");

        var result = await _service.Update(input);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}