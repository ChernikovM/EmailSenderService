using DataAccessLayer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Error : BaseEntity
    {
        [Required]
        public string FailMessage { get; set; }

        public Mail Mail { get; set; }
    }
}
