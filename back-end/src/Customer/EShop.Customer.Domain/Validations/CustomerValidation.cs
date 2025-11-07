using FluentValidation;
using EShop.Customer.Domain.AggreagatesModel.ValueObjects;

namespace EShop.Customer.Domain.Validations;

public class CustomerValidation : AbstractValidator<AggreagatesModel.Customer>
{
    public CustomerValidation()
    {
        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("O sobrenome é obrigatório.")
            .Length(2, 100).WithMessage("O sobrenome deve ter entre 2 e 100 caracteres.");

        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres.");

        RuleFor(c => c.PhoneNumber)
            .NotEmpty().WithMessage("O número de telefone é obrigatorio.")
            .GreaterThan(0).WithMessage("O número de telefone deve ser um número inteiro positivo.");

        RuleFor(c => c.Address)
            .NotNull().WithMessage("O endereço é obrigatório.")
            .SetValidator(new AddressValidation());
    }

    private sealed class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("O logradouro (rua) é obrigatório.")
                .Length(2, 200).WithMessage("O logradouro deve ter entre 2 e 200 caracteres.");

            RuleFor(a => a.Number)
                .NotEmpty().WithMessage("O número do endereço é obrigatório.")
                .MaximumLength(20).WithMessage("O número do endereço deve ter no máximo 20 caracteres.");

            RuleFor(a => a.Complement)
                .MaximumLength(100).WithMessage("O complemento deve ter no máximo 100 caracteres.")
                .When(a => !string.IsNullOrWhiteSpace(a.Complement));

            RuleFor(a => a.Neighborhood)
                .MaximumLength(100).WithMessage("O bairro deve ter no máximo 100 caracteres.")
                .When(a => !string.IsNullOrWhiteSpace(a.Neighborhood));

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("A cidade é obrigatória.")
                .Length(2, 100).WithMessage("A cidade deve ter entre 2 e 100 caracteres.");

            RuleFor(a => a.State)
                .NotEmpty().WithMessage("O estado é obrigatório.")
                .Length(2, 100).WithMessage("O estado deve ter entre 2 e 100 caracteres.");

            RuleFor(a => a.Country)
                .NotEmpty().WithMessage("O país é obrigatório.")
                .Length(2, 100).WithMessage("O país deve ter entre 2 e 100 caracteres.");

            RuleFor(a => a.PostalCode)
                .GreaterThan(0).WithMessage("O CEP deve ser um número inteiro positivo.");
        }
    }
}