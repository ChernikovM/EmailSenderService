using MimeKit;
using System.Collections.Generic;

namespace EmailSenderService.GraphQL.Inputs
{
    public record SendMailInput(string subject, string body, List<string> adresses);
}
