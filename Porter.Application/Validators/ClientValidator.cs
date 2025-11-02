using FluentValidation;
using Porter.Domain.Validators;
using Porter.Dto;

namespace Porter.Application.Validators
{
    public class RequestRegisterClientDtoValidator : AbstractValidator<RequestRegisterClientDto>
    {
        public RequestRegisterClientDtoValidator()
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
