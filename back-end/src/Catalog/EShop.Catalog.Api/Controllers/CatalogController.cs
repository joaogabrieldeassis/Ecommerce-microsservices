using EShop.Catalog.Api.Dtos.Requests;
using EShop.Catalog.Api.Dtos.Responses;
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

        return Ok(productsCatalog.Select(x => (ProductCatalogResponse?)x).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var productCatalog = await _catalogRepository.GetByIdAsync(id);

        return Ok((ProductCatalogResponse?)productCatalog);
    }

    [HttpGet("products-branch")]
    public async Task<IActionResult> GetAllProductsCatalogBranchAsync()
    {
        var productsBranchs = await _catalogRepository.GetAllProductsCatalogBranchAsync();

        return Ok(productsBranchs.Select(x => (ProductCatalogBrandResponse?)x).ToList());
    }    

    [HttpPost]
    public async Task<IActionResult> PostAsync(ProductCatalogRequest productCatalogDto)
    {
        var productCatalog = new ProductCatalog(
            productCatalogDto.Name,
            productCatalogDto.Description,
            productCatalogDto.Price,
            productCatalogDto.PictureFileName,
            productCatalogDto.PictureUri,
            productCatalogDto.AvailableStock,
            productCatalogDto.RestockThreshold,
            productCatalogDto.MaxStockThreshold,
            productCatalogDto.CatalogBrandId);

        await _catalogRepository.AddAsync(productCatalog);

        return Ok(productCatalog);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(ProductCatalogRequest productCatalogDto, Guid id)
    {
        var productCatalog = await _catalogRepository.GetByIdAsync(id);

        productCatalog?.Update(
            productCatalogDto.Name,
            productCatalogDto.Description,
            productCatalogDto.Price,
            productCatalogDto.PictureFileName,
            productCatalogDto.PictureUri,
            productCatalogDto.CatalogBrandId,
            productCatalogDto.AvailableStock,
            productCatalogDto.RestockThreshold,
            productCatalogDto.MaxStockThreshold
        );

        await _catalogRepository.UpdateAsync();

        return Ok(productCatalog);
    }
}