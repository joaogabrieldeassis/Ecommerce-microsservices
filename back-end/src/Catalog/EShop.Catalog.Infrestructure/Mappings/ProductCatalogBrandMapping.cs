using EShop.Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Catalog.Infrestructure.Mappings;

public class ProductCatalogBrandMapping : IEntityTypeConfiguration<ProductCatalogBrand>
{
    public void Configure(EntityTypeBuilder<ProductCatalogBrand> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("CatalogBrands");

        builder.HasData(new
        {
            Id = Guid.Parse("c9d4c053-49b6-410c-bc78-2d54a9991870"),
            Brand = "Nike"
        },
        new {
            Id = Guid.Parse("edef3c70-38fb-44b1-8028-e620c42b6c6f"),
            Brand = "Adidas"
        },
        new
        {
            Id = Guid.Parse("e42a20db-bd93-4897-829f-a2b436ceff7c"),
            Brand = "Louis Vuitton"
        });
    }
}