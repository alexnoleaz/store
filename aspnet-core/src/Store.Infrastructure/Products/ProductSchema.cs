using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Products;

public class ProductSchema : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(ProductConsts.MaxNameLength);
        builder.Property(p => p.Sku).HasMaxLength(ProductConsts.MaxSkuLength);
    }
}
