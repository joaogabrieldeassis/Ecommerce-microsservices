using EShop.Catalog.Domain.Models;

namespace EShop.Catalog.Api.Dtos.Responses;

public class ProductCatalogBrandResponse
{
    public ProductCatalogBrandResponse()   { }
    public ProductCatalogBrandResponse(string brand)
    {
        Id = Guid.NewGuid();
        Brand = brand;
    }

    public Guid Id { get; set; }
    public string Brand { get; set; } = string.Empty;


    public static implicit operator ProductCatalogBrandResponse(ProductCatalogBrand productCatalogBrand)
    {
        return new()
        {
            Id = productCatalogBrand.Id,
            Brand = productCatalogBrand.Brand
        };
    }
}