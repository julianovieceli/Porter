using FluentValidation;
using Porter.Application.Commands.Client;
using Porter.Domain.Validators;

namespace Porter.Application.Validators
{
    public class RegisterClientCommandValidator : AbstractValidator<RegisterClientCommand>
    {
        public RegisterClientCommandValidator()
        {
            RuleFor(client => client.Name).NotNull().NotEmpty().
                Custom(
                    (name, context) =>
                    {
                        if (!NameValidator.IsValidName(name))
                        {
                            context.AddFailure("Nome inválido.");
                        }
                    });

            RuleFor(client => client.Docto).NotNull().NotEmpty().
                Custom(
                    (docto, context) =>
                    {
                        if (!DocumentValidator.IsCpfCnpjValid(docto))
                        {
                            context.AddFailure("Documento é obrigatório e deve conter 11 dígitos (CPF) ou 14 dígitos (CNPJ).");
                        }
                    });
        }
    }
}
