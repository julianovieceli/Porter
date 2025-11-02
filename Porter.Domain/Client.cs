using System.ComponentModel.DataAnnotations.Schema;

namespace Porter.Domain
{

    public class Client : BaseDomain
    {
        public string Docto { get; set; }

        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
