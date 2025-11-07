using MediatR;
using Personal.Common;
using System.ComponentModel.DataAnnotations;

namespace Porter.Application.Commands.Booking
{
    public class RegisterBookingCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Sala obrigatória.")]
        public string RoomName { get; set; }

        [Required(ErrorMessage = "DoctoReservedBy obrigatório.")]
        public string DoctoReservedBy { get; set; }

        [Required(ErrorMessage = "StartDate obrigatório.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate obrigatório.")]
        public DateTime EndDate { get; set; }

        public string? Obs { get; set; }
    }

    public class UpdateBookingCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Id da reserva obrigatório .")]
        public int Id { get; set; }

        [Required(ErrorMessage = "StartDate obrigatório.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate obrigatório.")]
        public DateTime EndDate { get; set; }

        public string? Obs { get; set; }


    }

    public class DeleteBookingByIdCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }

}
