using Porter.Common.Domain;
using Porter.Common.Domain.ExtensionMethods;
using Porter.Domain.Validators;

namespace Porter.Domain
{
    public class Room: BaseDomain
    {
        private string _name;

        public string Name
        {

            get
            {
                return _name;
            }
            set
            {
                if (!NameValidator.IsValidName(value))
                    throw new Exception("Nome inválido!");

                _name = value;
            }
        }

        public virtual ICollection<Booking> Bookings { get; set; }

        public Room(string name)
        {
            _name = name;
            CreateTime = DateTime.SpecifyKind(DateTime.Now.ToBrazilDatetime(), DateTimeKind.Utc);
        }
    }
}
