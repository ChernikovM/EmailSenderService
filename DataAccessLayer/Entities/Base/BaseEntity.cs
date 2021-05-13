using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
