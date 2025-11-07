using MediatR;
using Personal.Common;
using System.ComponentModel.DataAnnotations;

namespace Porter.Application.Commands.Client
{
    public class RegisterClientCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Nome obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Docto obrigatório.")]
        public string Docto { get; set; }
    }
}
