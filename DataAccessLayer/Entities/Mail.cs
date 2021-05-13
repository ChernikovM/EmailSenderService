using DataAccessLayer.Entities.Base;
using System.ComponentModel.DataAnnotations;
using static DataAccessLayer.Enums.Enums;

namespace DataAccessLayer.Entities
{
    public class Mail : BaseEntity
    {
        public MailStatus Status { get; set; }

        [Required]
        public long MessageId { get; set; }

        public Message Message { get; set; }

        public long? ErrorId { get; set; }

        public Error Error { get; set; }

        public Mail()
        {
            Status = MailStatus.InProgress;
        }
    }
}
