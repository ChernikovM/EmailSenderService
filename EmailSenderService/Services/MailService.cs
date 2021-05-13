using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.EFRepository.Interfaces;
using EmailSenderService.Configurations.Interfaces;
using EmailSenderService.GraphQL.Payloads;
using EmailSenderService.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static DataAccessLayer.Enums.Enums;

namespace EmailSenderService.Services
{
    public class MailService : IMailService
    {
        public ISmtpConfiguration SmtpConfig { get; set; }

        private readonly IMessageRepository _messageRepository;
        private readonly IMailRepository _mailRepossitory;
        private readonly IErrorRepository _errorRepository;

        public MailService(
            ISmtpConfiguration config, 
            IMessageRepository messageRepository,
            IMailRepository mailRepossitory,
            IErrorRepository errorRepository
            )
        {
            SmtpConfig = config;
            _messageRepository = messageRepository;
            _mailRepossitory = mailRepossitory;
            _errorRepository = errorRepository;
        }

        public async Task<SendMailPayload> SendGroupMailAsync(string subject, string body, List<string> addresses, CancellationToken cancellationToken)
        {
            Mail newMail;

            try
            {
                var message = new Message() { Subject = subject, BodyText = body };
                var messageId = await _messageRepository.CreateAsync(message);

                newMail = new Mail() { Message = message };
                await _mailRepossitory.CreateAsync(newMail);
            }
            catch (Exception ex)
            {
                return new SendMailPayload(null, $"Failed. Mail was not created. Error: {ex.Message}");
            }

            try
            {
                using var client = await GetClientAsync(cancellationToken);

                var mail = CreateGroupMail(subject, body, addresses);

                await client.SendAsync(mail, cancellationToken: cancellationToken);

                await client.DisconnectAsync(true, cancellationToken);
            }
            catch(Exception ex)
            {
                var error = await HandleException(ex, newMail);

               return new SendMailPayload(newMail, error.FailMessage);
            }

            newMail.Status = MailStatus.Successfully;
            await _mailRepossitory.UpdateAsync(newMail);

            return new SendMailPayload(newMail, null);
        }

        private async Task<SmtpClient> GetClientAsync(CancellationToken cancellationToken)
        {
            var client = new SmtpClient();

            await client.ConnectAsync(SmtpConfig.Host, SmtpConfig.Port, true, cancellationToken: cancellationToken);

            if (client.Capabilities.HasFlag(SmtpCapabilities.Authentication))
            {
                await client.AuthenticateAsync(SmtpConfig.Username, SmtpConfig.Password, cancellationToken);
            }

            return client;
        }

        private MimeMessage CreateGroupMail(string subject, string body, List<string> addresses)
        {
            var emailMessage = new MimeMessage();

            var addressList = new List<MailboxAddress>();

            addresses.ForEach(x =>
            {
                if (MailboxAddress.TryParse(x, out var address))
                {
                    addressList.Add(address);
                }
            });

            emailMessage.From.Add(new MailboxAddress(SmtpConfig.FromName, SmtpConfig.FromAddress));
            emailMessage.To.AddRange(addressList);
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart()
            {
                Text = body,
            };            

            return emailMessage;
        }

        private async Task<Error> HandleException(Exception ex, Mail mail)
        {
            var error = new Error() { FailMessage = ex.Message, Mail = mail};

            await _errorRepository.CreateAsync(error);

            mail.Status = MailStatus.Failed;
            await _mailRepossitory.UpdateAsync(mail);

            return error;
        }
    }
}
