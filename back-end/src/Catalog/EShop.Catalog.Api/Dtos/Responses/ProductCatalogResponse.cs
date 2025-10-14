using EShop.Catalog.Domain.Models;

namespace EShop.Catalog.Api.Dtos.Responses;

public class ProductCatalogResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string PictureUri { get; init; } = string.Empty;

    public static implicit operator ProductCatalogResponse?(ProductCatalog? productCatalog)
    {
        if (productCatalog == null) return null;

        return new()
        {
            Id = productCatalog.Id,
            Name = productCatalog.Name,
            Description = productCatalog.Description,
            Price = productCatalog.Price,
            PictureUri = productCatalog.PictureUri
        };
    }
}