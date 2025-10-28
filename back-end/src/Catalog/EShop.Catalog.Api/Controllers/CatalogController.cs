using EShop.Catalog.Api.Dtos.Requests;
using EShop.Catalog.Api.Dtos.Responses;
using EShop.Catalog.Api.IntegrationsEvent;
using EShop.Catalog.Domain.Interfaces;
using EShop.Catalog.Domain.Models;
using EShop.Shared.EventBus.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Catalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController(ICatalogRepository catalogRepository, IMessageBus eventBus) : ControllerBase
{
    private readonly ICatalogRepository _catalogRepository = catalogRepository;
    private readonly IMessageBus _eventBus = eventBus;

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

    [HttpPost]
    public async Task<IActionResult> PostAsync(ProductCatalogRequest productCatalogDto)
    {
        var productCatalog = new ProductCatalog(productCatalogDto.Name,
                                                productCatalogDto.Description,
                                                productCatalogDto.Price,
                                                productCatalogDto.PictureUri,
                                                productCatalogDto.Quantity);

        await _catalogRepository.AddAsync(productCatalog);

        var @event = new ProductCreatedIntegrationEvent(productCatalog.Name,
                                                        productCatalog.QuantityInStock,
                                                        productCatalog.Price);
        await _eventBus.PublishAsync(@event);

        return Ok(productCatalog);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(ProductCatalogRequest productCatalogDto, Guid id)
    {
        var productCatalog = await _catalogRepository.GetByIdAsync(id);

        productCatalog?.Update(productCatalogDto.Name,
                               productCatalogDto.Description,
                               productCatalogDto.Price,
                               productCatalogDto.PictureUri);

        await _catalogRepository.UpdateAsync();

        return Ok(productCatalog);
    }
}