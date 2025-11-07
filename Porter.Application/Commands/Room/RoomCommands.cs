using MediatR;
using Personal.Common;
using Personal.Common.Domain;
using System.ComponentModel.DataAnnotations;

namespace Porter.Application.Commands.Booking
{
    public class RegisterRoomCommand: IRequest<Result>
    {
        [Required(ErrorMessage = "Nome obrigatório.")]
        public string Name { get; set; }

    }




}
