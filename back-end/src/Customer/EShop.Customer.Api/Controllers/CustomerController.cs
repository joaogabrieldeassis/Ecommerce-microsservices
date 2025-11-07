using EShop.Customer.Application;
using EShop.Customer.Application.Dtos;
using EShop.Shared.Api.Controllers;
using EShop.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Customer.Api.Controllers;

[Route("[controller]")]
public class CustomerController(INotifier notifier,
                                ICustomerApplication customerApplication) : MainController(notifier)
{
    private readonly ICustomerApplication _customerApplication = customerApplication;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _customerApplication.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(await _customerApplication.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CustomerDto customerDto)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _customerApplication.CreateAsync(customerDto);

        return CustomResponse();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, CustomerDto customerDto)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _customerApplication.UpdateAsync(id, customerDto);

        return CustomResponse();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _customerApplication.DeleteAsync(id);

        return CustomResponse();
    }
}