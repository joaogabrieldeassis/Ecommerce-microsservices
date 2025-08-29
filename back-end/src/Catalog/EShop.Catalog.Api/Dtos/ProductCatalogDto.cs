using EShop.Catalog.Domain.Models;
using EShop.Catalog.Domain.Models.Enuns;

namespace EShop.Catalog.Api.Dtos;

public class ProductCatalogDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string PictureFileName { get; init; } = string.Empty;
    public string PictureUri { get; init; } = string.Empty;
    public int CatalogTypeId { get; init; }
    public CatalogType CatalogType { get; init; }
    public Guid CatalogBrandId { get; set; }
    public ProductCatalogBrandDto ProductCatalogBrandDto { get; init; } = new();
    public int AvailableStock { get; init; }
    public int RestockThreshold { get; init; }
    public int MaxStockThreshold { get; init; }
    public bool OnReorder { get; init; }


    public static implicit operator ProductCatalogDto?(ProductCatalog? productCatalog)
    {
        if (productCatalog == null) return null;

        return new()
        {
            Id = productCatalog.Id,
            Name = productCatalog.Name,
            Description = productCatalog.Description,
            Price = productCatalog.Price,
            PictureFileName = productCatalog.PictureFileName,
            PictureUri = productCatalog.PictureUri,
            CatalogTypeId = productCatalog.CatalogTypeId,
            CatalogType = productCatalog.CatalogType,
            ProductCatalogBrandDto = productCatalog.ProductCatalogBrand,
            AvailableStock = productCatalog.AvailableStock,
            RestockThreshold = productCatalog.RestockThreshold,
            MaxStockThreshold = productCatalog.MaxStockThreshold,
            OnReorder = productCatalog.OnReorder
        };
    }

}