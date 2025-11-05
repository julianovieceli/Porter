using FluentValidation;
using Porter.Application.Commands.Booking;
using Porter.Domain.Validators;

namespace Porter.Application.Validators
{
    public class RequestRegisterBookingValidator : AbstractValidator<RegisterBookingCommand>
    {
        public RequestRegisterBookingValidator()
        {
            RuleFor(booking => booking.DoctoReservedBy).NotNull().NotEmpty().
               Custom(
                   (docto, context) =>
                   {
                       if (!DocumentValidator.IsCpfCnpjValid(docto))
                       {
                           context.AddFailure("Documento d reserva é obrigatório e deve conter 11 dígitos (CPF) ou 14 dígitos (CNPJ).");
                       }
                   });

            RuleFor(booking => booking.StartDate).NotNull().NotEmpty().
              Custom(
                  (startdate, context) =>
                  {
                      if (startdate < DateTime.UtcNow.ToLocalTime())
                          context.AddFailure("Data de início deve ser maior ou igual a data atual!.");
                  });

            RuleFor(x => x)
            .Must(x => x.EndDate > x.StartDate)
            .WithMessage("Data de início deve ser menor que a data de fim!")
            .When(x => x.StartDate != default && x.EndDate != default); // Apply only if both dates are provided



            RuleFor(booking => booking.RoomName).NotNull().NotEmpty().WithMessage("Sala obrigatória.");
        }
    }

    public class RequestUpdateBookingValidator : AbstractValidator<UpdateBookingCommand>
    {
        public RequestUpdateBookingValidator()
        {
            
            RuleFor(booking => booking.StartDate).NotNull().NotEmpty().
              Custom(
                  (startdate, context) =>
                  {
                      if (startdate < DateTime.UtcNow)
                          context.AddFailure("Data de início deve ser maior ou igual a data atual!.");
                  });

            RuleFor(x => x)
            .Must(x => x.EndDate > x.StartDate)
            .WithMessage("Data de início deve ser menor que a data de fim!")
            .When(x => x.StartDate != default && x.EndDate != default); // Apply only if both dates are provided
        }
    }
}
