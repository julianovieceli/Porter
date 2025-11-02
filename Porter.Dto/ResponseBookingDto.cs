namespace Porter.Dto
{
    public class ResponseBookingDto
    {
        public int Id { get; set; }

        public string ReservedBy { get; set; }

        public string Room { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
