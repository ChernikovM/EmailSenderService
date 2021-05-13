using DataAccessLayer.Entities;

namespace EmailSenderService.GraphQL.Payloads
{
    public record SendMailPayload(Mail mail, string error = null);
}
