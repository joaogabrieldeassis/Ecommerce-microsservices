using EShop.Customer.Application.Dtos;
using EShop.Customer.Infra.Data;
using EShop.Shared.Interfaces;
using EShop.Shared.Notifications;
using Microsoft.EntityFrameworkCore;

namespace EShop.Customer.Application;

public class CustomerApplication(CustomerContext context, INotifier notifier) : ICustomerApplication
{
    private readonly CustomerContext _context = context;
    private readonly INotifier _notifier = notifier;

    public async Task<IEnumerable<CustomerDto>?> GetAllAsync()
    {
        var customers = await _context.Customers.ToListAsync();

        return customers?.Select(customer =>
        {
            CustomerDto customerDto = customer;

            return customerDto;
        });
    }

    public async Task<CustomerDto?> GetByIdAsync(Guid id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

        CustomerDto? customerDto = customer;

        return customerDto;
    }

    public async Task CreateAsync(CustomerDto customerDto)
    {
        var customer = new Domain.AggreagatesModel.Customer(customerDto.LastName,
                                                            customerDto.FirstName,
                                                            customerDto.PhoneNumber,
                                                            customerDto.Address.Street,
                                                            customerDto.Address.Number,
                                                            customerDto.Address.Complement,
                                                            customerDto.Address.Neighborhood,
                                                            customerDto.Address.City,
                                                            customerDto.Address.State,
                                                            customerDto.Address.Country,
                                                            customerDto.Address.PostalCode);

        if (!CustomerIsValid(customer)) return;

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, CustomerDto dto)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        if (customer == null)
        {
            _notifier.Handle(new Notification("Cliente não encontrado."));
            return;
        }

        customer.Update(dto.LastName,
                        dto.FirstName,
                        dto.PhoneNumber,
                        dto.Address.Street,
                        dto.Address.Number,
                        dto.Address.Complement,
                        dto.Address.Neighborhood,
                        dto.Address.City,
                        dto.Address.State,
                        dto.Address.Country,
                        dto.Address.PostalCode);

        if (!CustomerIsValid(customer)) return;

        _context.Customers.Update(customer);    
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        if (customer == null)
        {
            _notifier.Handle(new Notification("Cliente não encontrado."));
            return;
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public bool CustomerIsValid(Domain.AggreagatesModel.Customer customer)
    {
        var validationResult = customer.IsValid();
        if (validationResult.IsValid) return true;

        foreach (var error in validationResult!.Errors)
        {
            _notifier.Handle(new Notification(error.ErrorMessage));
        }
        return false;
    }
}