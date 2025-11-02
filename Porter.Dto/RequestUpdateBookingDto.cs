using System.ComponentModel.DataAnnotations;

namespace Porter.Dto
{
    public class RequestUpdateBookingDto
    {
        [Required(ErrorMessage = "Id da reserva obrigatório .")]
        public int Id { get; set; }

        [Required(ErrorMessage = "StartDate obrigatório.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate obrigatório.")]
        public DateTime EndDate { get; set; }

        public string? Obs { get; set; }


    }
}
