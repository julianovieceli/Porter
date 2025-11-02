using System.ComponentModel.DataAnnotations;

namespace Porter.Dto
{
    public class RequestRegisterClientDto
    {
        [Required( ErrorMessage = "Nome obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Docto obrigatório.")]
        public string Docto { get; set; }

    }
}
