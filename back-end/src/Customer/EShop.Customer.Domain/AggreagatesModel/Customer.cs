using EShop.Customer.Domain.AggreagatesModel.ValueObjects;
using EShop.Shared.Entities;
using EShop.Shared.Interfaces;

namespace EShop.Customer.Domain.AggreagatesModel;

public class Customer : Entity, IAggregateRoot
{
    public Customer(string lastName,
                    string firstName,
                    int phoneNumber,
                    string street,
                    string number,
                    string complement,
                    string neighborhood,
                    string city,
                    string state,
                    string country,
                    int postalCode)
    {
        LastName = lastName;
        FirstName = firstName;
        PhoneNumber = phoneNumber;
        Address = new Address(street,
                              number,
                              complement,
                              neighborhood,
                              city,
                              state,
                              country,
                              postalCode);
    }

    public string LastName { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public int PhoneNumber { get; private set; }
    public Address Address { get; private set; } = new Address();
}