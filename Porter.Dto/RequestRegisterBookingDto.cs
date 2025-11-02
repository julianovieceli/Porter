using System.ComponentModel.DataAnnotations;

namespace Porter.Dto
{
    public class RequestRegisterBookingDto
    {
        
        [Required( ErrorMessage = "Sala obrigatória.")]
        public string RoomName { get; set; }

        [Required(ErrorMessage = "DoctoReservedBy obrigatório.")]
        public string DoctoReservedBy { get; set; }

        [Required(ErrorMessage = "StartDate obrigatório.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate obrigatório.")]
        public DateTime EndDate { get; set; }

        public string? Obs { get; set; }


    }
}
