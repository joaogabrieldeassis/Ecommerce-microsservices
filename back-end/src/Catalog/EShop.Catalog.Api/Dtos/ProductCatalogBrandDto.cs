using EShop.Catalog.Domain.Models;

namespace EShop.Catalog.Api.Dtos;

public class ProductCatalogBrandDto
{
    public ProductCatalogBrandDto()   { }
    public ProductCatalogBrandDto(string brand)
    {
        Id = Guid.NewGuid();
        Brand = brand;
    }

    public Guid Id { get; set; }
    public string Brand { get; set; } = string.Empty;


    public static implicit operator ProductCatalogBrandDto(ProductCatalogBrand productCatalogBrand)
    {
        return new()
        {
            Id = productCatalogBrand.Id,
            Brand = productCatalogBrand.Brand
        };
    }
}