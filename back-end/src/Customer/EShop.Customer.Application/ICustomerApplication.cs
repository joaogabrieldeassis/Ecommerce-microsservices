using EShop.Customer.Application.Dtos;

namespace EShop.Customer.Application;

public interface ICustomerApplication
{
    Task<IEnumerable<CustomerDto>> GetAllAsync();
    Task<CustomerDto?> GetByIdAsync(Guid id);
    Task CreateAsync(Domain.AggreagatesModel.Customer entity);
    Task UpdateAsync(Domain.AggreagatesModel.Customer entity);
    Task DeleteAsync(Guid id);
}