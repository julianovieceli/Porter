using Personal.Common.Domain;
using Personal.Common.Domain.ExtensionMethods;
using Porter.Domain.Validators;

namespace Porter.Domain
{

    public class Client : BaseDomain
    {

        private string _docto;
        private string _name;

        public string Docto {
            get { return _docto; }
            set
            {
                if (!DocumentValidator.IsCpfCnpjValid(value))
                    throw new Exception("Documento inválido!");

                _docto = value;
            }
        }

        public string Name {

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

        public Client(string name, string docto)
        {
            Name = name;
            Docto = docto;
            CreateTime = DateTime.SpecifyKind(DateTime.Now.ToBrazilDatetime(), DateTimeKind.Utc);//   DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

        }
    }
}
