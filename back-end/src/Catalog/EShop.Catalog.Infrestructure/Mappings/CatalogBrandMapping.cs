using EShop.Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Catalog.Infrestructure.Mappings;

public class CatalogBrandMapping : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("CatalogBrands");
    }
}