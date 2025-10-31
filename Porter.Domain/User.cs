using System.ComponentModel.DataAnnotations.Schema;

namespace Porter.Domain
{

    public class UserPorter: BaseDomain
    {
        public string Login { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
