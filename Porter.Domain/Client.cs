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


        public Client(string name, string docto)
        {
            Name = name;
            Docto = docto;
            CreateTime = DateTime.UtcNow;

        }
    }
}
