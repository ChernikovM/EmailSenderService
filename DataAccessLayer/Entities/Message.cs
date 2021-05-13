using DataAccessLayer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Message : BaseEntity
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string BodyText { get; set; }

        public Mail Mail { get; set; }
    }
}
