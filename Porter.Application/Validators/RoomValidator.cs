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
                        if (name.Length < 5)
                        {
                            context.AddFailure("Nome da sala precisa ter 5 caracteres ao menos.");
                        }
                    });

          
        }
    }
}
