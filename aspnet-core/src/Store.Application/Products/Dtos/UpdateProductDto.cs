namespace Store.Products.Dtos;

public class UpdateProductDto
{
    public int Id { get; set; }
    public string Sku { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
}
