using EShop.Customer.Application.Dtos;

namespace EShop.Customer.Application;

public class CustomerApplication : ICustomerApplication
{
    public Task CreateAsync(Domain.AggreagatesModel.Customer entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CustomerDto?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.AggreagatesModel.Customer entity)
    {
        throw new NotImplementedException();
    }
}