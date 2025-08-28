using EShop.Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Catalog.Infrestructure.Mappings;

public class CatalogBrandMapping : IEntityTypeConfiguration<ProductCatalogBrand>
{
    public void Configure(EntityTypeBuilder<ProductCatalogBrand> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("CatalogBrands");
    }
}