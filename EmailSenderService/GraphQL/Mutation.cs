using EmailSenderService.GraphQL.Inputs;
using EmailSenderService.GraphQL.Payloads;
using EmailSenderService.Services.Interfaces;
using HotChocolate;
using System.Threading;
using System.Threading.Tasks;

namespace EmailSenderService.GraphQL
{
    public class Mutation
    {
        public async Task<SendMailPayload> SendGroupMail(
            SendMailInput input,
            [Service]IMailService _mailService,
            CancellationToken cancellationToken)
        {
            var result = await _mailService.SendGroupMailAsync(input.subject, input.body, input.adresses, cancellationToken);

            return result;
        }
    }
}
