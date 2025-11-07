namespace EShop.Customer.Application.Dtos;

public class CustomerDto
{
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public int PhoneNumber { get; set; }
    public AddressDto Address { get; set; } = new();

    public static implicit operator CustomerDto(Domain.AggreagatesModel.Customer? customer)
    {
        if (customer is null) return null!;

        return new CustomerDto
        {
            LastName = customer.LastName,
            FirstName = customer.FirstName,
            PhoneNumber = customer.PhoneNumber,
            Address = new AddressDto
            {
                Street = customer.Address.Street,
                Number = customer.Address.Number,
                Complement = customer.Address.Complement,
                Neighborhood = customer.Address.Neighborhood,
                City = customer.Address.City,
                State = customer.Address.State,
                Country = customer.Address.Country,
                PostalCode = Convert.ToInt32(customer.Address.PostalCode)
            }
        };
    }
}