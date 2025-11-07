using EShop.Customer.Domain.AggreagatesModel.ValueObjects;
using EShop.Customer.Domain.Validations;
using EShop.Shared.Entities;
using EShop.Shared.Interfaces;
using FluentValidation.Results;

namespace EShop.Customer.Domain.AggreagatesModel;

public class Customer(string lastName,
                      string firstName,
                      int phoneNumber,
                      string street,
                      string number,
                      string complement,
                      string neighborhood,
                      string city,
                      string state,
                      string country,
                      int postalCode) : Entity, IAggregateRoot
{
    public string LastName { get; private set; } = lastName;
    public string FirstName { get; private set; } = firstName;
    public int PhoneNumber { get; private set; } = phoneNumber;
    public Address Address { get; private set; } = new Address(street,
                                                               number,
                                                               complement,
                                                               neighborhood,
                                                               city,
                                                               state,
                                                               country,
                                                               postalCode);

    public void Update(string lastName,
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

        Address.Update(street,
                       number,
                       complement,
                       neighborhood,
                       city,
                       state,
                       country,
                       postalCode);

        UpdateDate = DateTime.UtcNow;
    }

    public ValidationResult IsValid()
    {
        var validator = new CustomerValidation();
        ValidationResult validation = validator.Validate(this);

        return validation;
    }
}