using System.ComponentModel.DataAnnotations;

namespace Porter.Dto
{
    public class RequestRegisterRoomDto
    {
        [Required( ErrorMessage = "Nome obrigatório.")]
        public string Name { get; set; }

    }
}
