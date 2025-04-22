using Store.Shared.Entities;

namespace Store.Products;

public class Product : Entity<int>
{
    public string Sku { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
}
