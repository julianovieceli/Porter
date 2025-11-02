using System.ComponentModel.DataAnnotations;

namespace Porter.Domain
{
    public abstract class BaseDomain
    {
        [Key]
        public int Id { get; set; }


        public DateTime CreateTime { get; set; }
    }
}
