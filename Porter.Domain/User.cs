using System.ComponentModel.DataAnnotations.Schema;

namespace Porter.Domain
{

    public class UserPorter: BaseDomain
    {
        public string Docto { get; set; }

        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
