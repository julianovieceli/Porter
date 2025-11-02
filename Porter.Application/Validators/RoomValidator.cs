using FluentValidation;
using Porter.Domain.Validators;
using Porter.Dto;

namespace Porter.Application.Validators
{
    public class RequestRegisterRoomDtoValidator : AbstractValidator<RequestRegisterRoomDto>
    {
        public RequestRegisterRoomDtoValidator()
        {
            RuleFor(room => room.Name).NotNull().NotEmpty().
                Custom(
                    (name, context) =>
                    {
                        if (!NameValidator.IsValidName(name))
                        {
                            context.AddFailure("Nome inválido.");
                        }
                    });

          
        }
    }
}
