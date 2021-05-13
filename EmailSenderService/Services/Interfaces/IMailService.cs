using DataAccessLayer.AppDataContext;
using EmailSenderService.Configurations.Interfaces;
using EmailSenderService.GraphQL.Payloads;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmailSenderService.Services.Interfaces
{
    public interface IMailService
    {
        ISmtpConfiguration SmtpConfig { get; set; }

        Task<SendMailPayload> SendGroupMailAsync(string subject, string body, List<string> addresses, CancellationToken cancellationToken);
    }
}
