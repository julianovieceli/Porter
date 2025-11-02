namespace Porter.Domain
{
    public class Booking: BaseDomain
    {
        public string? Obs { get; set; }

        public  int RoomId { get; set; }
        public  virtual Room Room { get; set; }

        public virtual int ReservedById { get; set; }
        public virtual Client ReservedBy { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public Booking()
        {
            
        }

        public Booking(Room room , Client reservedBy, DateTime startDate, DateTime endDate, string obs)
        {
            if (startDate >= endDate)
                throw new Exception("Data de início deve ser menor que a data de fim!");

            if (startDate < DateTime.UtcNow)
                throw new Exception("Data de início deve ser maior ou igual a data atual!");

            if(room == null)
                throw new Exception("Sala inválida!");

            if (reservedBy == null)
                throw new Exception("Cliente inválido!");

            Room = room;
            ReservedBy = reservedBy;
            StartDate = startDate;
            EndDate = endDate;
            Obs = obs;
            CreateTime = DateTime.UtcNow;
        }
    }
}
