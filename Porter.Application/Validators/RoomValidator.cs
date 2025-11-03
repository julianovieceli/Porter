using FluentValidation;
using Porter.Application.Commands.Booking;

namespace Porter.Application.Validators
{
    public class RegisterRoomCommandValidator : AbstractValidator<RegisterRoomCommand>
    {
        public RegisterRoomCommandValidator()
        {
            RuleFor(room => room.Name).NotNull().NotEmpty().
                Custom(
                    (name, context) =>
                    {
                        if ((name.Length < 5) || (name.Contains(" ")))
                        {
                            context.AddFailure("Nome da sala precisa ter 5 caracteres ao menos e não pode ter espaço.");
                        }
                    });

          
        }
    }
}
