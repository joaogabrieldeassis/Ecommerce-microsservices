namespace EShop.Customer.Domain.AggreagatesModel.ValueObjects;

public sealed class Address
{
    public Address() { }

    public Address(string street,
                   string number,
                   string complement,
                   string neighborhood,
                   string city,
                   string state,
                   string country,
                   int postalCode)
    {
        Street = street;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        PostalCode = postalCode;
    }

    public string Street { get; private set; } = string.Empty;         // Rua / Logradouro
    public string Number { get; private set; } = string.Empty;            // Número
    public string Complement { get; private set; } = string.Empty;        // Complemento (ex: apto, bloco)
    public string Neighborhood { get; private set; } = string.Empty;      // Bairro / Distrito
    public string City { get; private set; } = string.Empty;            // Cidade
    public string State { get; private set; } = string.Empty;           // Estado / Província
    public string Country { get; private set; } = string.Empty;            // País
    public int PostalCode { get; private set; }        // CEP / Código postal

    public void Update(string street,
                       string number,
                       string complement,
                       string neighborhood,
                       string city,
                       string state,
                       string country,
                       int postalCode)
    {
        Street = street;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        PostalCode = postalCode;
    }
}