using EShop.Catalog.Api.Dtos;
using EShop.Catalog.Domain.Interfaces;
using EShop.Catalog.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Catalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController(ICatalogRepository catalogRepository) : ControllerBase
{
    private readonly ICatalogRepository _catalogRepository = catalogRepository;

    [HttpGet]
    public async Task<IActionResult> GetItemsAsync()
    {
        var productsCatalog = await _catalogRepository.GetAllAsync();

        return Ok(productsCatalog.Select(x => (ProductCatalogDto?)x).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var productCatalog = await _catalogRepository.GetByIdAsync(id);

        return Ok((ProductCatalogDto?)productCatalog);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(ProductCatalogDto productCatalogDto)
    {
        var productCatalog = new ProductCatalog(
            productCatalogDto.Name,
            productCatalogDto.Description,
            productCatalogDto.Price,
            productCatalogDto.PictureFileName,
            productCatalogDto.PictureUri,
            productCatalogDto.CatalogTypeId,
            productCatalogDto.AvailableStock,
            productCatalogDto.RestockThreshold,
            productCatalogDto.MaxStockThreshold,
            productCatalogDto.CatalogBrandId);

        await _catalogRepository.AddAsync(productCatalog);

        return Ok(productCatalog);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(ProductCatalogDto productCatalogDto, Guid id)
    {
        var productCatalog = await _catalogRepository.GetByIdAsync(id);

        productCatalog?.Update(
            productCatalogDto.Name,
            productCatalogDto.Description,
            productCatalogDto.Price,
            productCatalogDto.PictureFileName,
            productCatalogDto.PictureUri,
            productCatalogDto.CatalogTypeId,
            productCatalogDto.CatalogType,
            productCatalogDto.CatalogBrandId,
            productCatalogDto.AvailableStock,
            productCatalogDto.RestockThreshold,
            productCatalogDto.MaxStockThreshold,
            productCatalogDto.OnReorder
        );

        await _catalogRepository.UpdateAsync();

        return Ok(productCatalog);
    }
}

