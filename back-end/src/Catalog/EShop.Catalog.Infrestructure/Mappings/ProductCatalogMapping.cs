using EShop.Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Catalog.Infrestructure.Mappings;

public class ProductCatalogMapping : IEntityTypeConfiguration<ProductCatalog>
{
    public void Configure(EntityTypeBuilder<ProductCatalog> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Price)
               .HasPrecision(28, 2);

        builder.HasOne(builder => builder.ProductCatalogBrand)
               .WithMany()
               .HasForeignKey(builder => builder.CatalogBrandId);

        builder.ToTable("CatalogItens");
    }
}