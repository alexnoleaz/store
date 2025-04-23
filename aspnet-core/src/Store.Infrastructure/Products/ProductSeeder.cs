using Store.Shared.Repositories.EntityFrameworkCore;

namespace Store.Products;

public static class ProductSeeder
{
    public static async Task SeedAsync(StoreDbContext context)
    {
        if (context.Products.Any())
            return;

        var products = Enumerable
            .Range(1, 30)
            .Select(i => new Product
            {
                Sku = $"SKU{i:000}",
                Name = $"Product {i}",
                Stock = 10 * i,
                Price = 9.99m + i,
                Status = ProductStatus.Active,
            });

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
