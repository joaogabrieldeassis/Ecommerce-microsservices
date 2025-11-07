using EShop.Customer.Application.Dtos;

namespace EShop.Customer.Application;

public interface ICustomerApplication
{
    Task<IEnumerable<CustomerDto>?> GetAllAsync();
    Task<CustomerDto?> GetByIdAsync(Guid id);
    Task CreateAsync(CustomerDto dto);
    Task UpdateAsync(Guid id, CustomerDto dto);
    Task DeleteAsync(Guid id);
}