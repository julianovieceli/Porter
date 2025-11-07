using Personal.Common.Domain;
using Personal.Common.Domain.ExtensionMethods;
using System.Text.Json.Serialization;

namespace Porter.Domain
{
    public class Booking: BaseDomain
    {
        public string? Obs { get; set; }

        public  int RoomId { get; set; }

        [JsonIgnore]
        public  virtual Room Room { get; set; }

        public virtual int ReservedById { get; set; }

        [JsonIgnore]
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

            if (startDate < DateTime.Now.ToBrazilDatetime())
                throw new Exception("Data de início deve ser maior ou igual a data atual!");

            if(room == null)
                throw new Exception("Sala inválida!");

            if (reservedBy == null)
                throw new Exception("Cliente inválido!");

            Room = room;
            ReservedBy = reservedBy;
            StartDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            EndDate = DateTime.SpecifyKind(endDate,  DateTimeKind.Utc); // DateTime.SpecifyKind(endDate, DateTimeKind.Utc); ;
            Obs = obs;
            CreateTime = DateTime.SpecifyKind(DateTime.Now.ToBrazilDatetime(), DateTimeKind.Utc);//   DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        }

        public void Update(DateTime startDate, DateTime endDate, string obs)
        {
            if (startDate >= endDate)
                throw new Exception("Data de início deve ser menor que a data de fim!");

            if (startDate < DateTime.Now.ToBrazilDatetime())
                throw new Exception("Data de início deve ser maior ou igual a data atual!");


            StartDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            EndDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc); ;
            Obs = obs;
        }

        public void Detete()
        {
            if(this.DeletedDate.HasValue)
                throw new Exception("Reserva já está deletada!");

            this.DeletedDate = DateTime.Now.ToUniversalTime();
        }
    }
}
