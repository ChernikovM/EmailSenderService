namespace EmailSenderService.Configurations.Interfaces
{
    public interface ISmtpConfiguration
    {
        string Host { get; }

        int Port { get; }

        bool UseSsl { get; }

        string Username { get; }

        string Password { get; }

        string FromName { get; }

        string FromAddress { get; }

    }
}
