using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {

        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddUserSecrets<Program>()
            .Build();

        var settings = config.Get<AppSettings>()
            ?? throw new InvalidOperationException("Unable to load application configuration.");
        settings.Validate();

        for (int i = 0; i < 1; i++)
        {
            SendMail(settings);
        }

    }

    private static void SendMail(AppSettings settings)
    {
        var smtpSettings = settings.SmtpServer;

        // Crea un nuovo oggetto MailMessage
        using var mail = new MailMessage();

        // Imposta il mittente della mail
        mail.Sender = new MailAddress(settings.Mail.Sender);
        mail.From = new MailAddress(settings.Mail.From);

        // Aggiungi uno o più destinatari della mail
        mail.To.Add(settings.Mail.To);

        // Imposta l'oggetto della mail
        mail.Subject = "Questa è una mail di prova";

        // Imposta il corpo della mail, che può essere in formato HTML o testo semplice
        mail.Body = $"{DateTime.Now} - Ciao, questa è una mail inviata da un programma in C#";
        mail.IsBodyHtml = false;

        // Crea un nuovo oggetto SmtpClient
        using var smtp = new SmtpClient();

        // Imposta il server SMTP che userai per inviare la mail
        smtp.Host = smtpSettings.Hostname;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Port = smtpSettings.Port;
        smtp.EnableSsl = smtpSettings.EnableSsl;
        smtp.Timeout = smtpSettings.TimeoutMilliseconds;
       
        if (!string.IsNullOrWhiteSpace(smtpSettings.Username))
        {
            // Imposta le credenziali di autenticazione, se richieste dal server SMTP
            smtp.Credentials = new NetworkCredential(
                smtpSettings.Username,
                smtpSettings.Password);
        }


        try
        {
            // Invia la mail
            smtp.Send(mail);
            Console.WriteLine($"Mail Sent on port:{smtpSettings.Port}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + $" Port:{smtpSettings.Port}");
        }
    }

internal sealed class AppSettings
{
    public MailSettings Mail { get; init; } = new();

    public SmtpServerSettings SmtpServer { get; init; } = new();

    public void Validate()
    {
        ValidateEmailAddress(Mail.Sender, "Mail:Sender");
        ValidateEmailAddress(Mail.From, "Mail:From");
        ValidateEmailAddress(Mail.To, "Mail:To");
        ValidateRequiredValue(SmtpServer.Hostname, "SmtpServer:Hostname");

        if (Uri.CheckHostName(SmtpServer.Hostname) is UriHostNameType.Unknown)
        {
            throw new InvalidOperationException(
                "Configuration value 'SmtpServer:Hostname' must be a valid DNS name or IP address.");
        }

        if (SmtpServer.Port is < 1 or > 65535)
        {
            throw new InvalidOperationException(
                "Configuration value 'SmtpServer:Port' must be between 1 and 65535.");
        }

        if (SmtpServer.TimeoutMilliseconds <= 0)
        {
            throw new InvalidOperationException(
                "Configuration value 'SmtpServer:TimeoutMilliseconds' must be greater than zero.");
        }

        var hasUsername = !string.IsNullOrWhiteSpace(SmtpServer.Username);
        var hasPassword = !string.IsNullOrWhiteSpace(SmtpServer.Password);

        if (hasUsername != hasPassword)
        {
            throw new InvalidOperationException(
                "Configuration values 'SmtpServer:Username' and 'SmtpServer:Password' must be provided together.");
        }
    }

    private static void ValidateRequiredValue(string value, string configurationKey)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException(
                $"Configuration value '{configurationKey}' is required.");
        }
    }

    private static void ValidateEmailAddress(string value, string configurationKey)
    {
        ValidateRequiredValue(value, configurationKey);

        try
        {
            _ = new MailAddress(value);
        }
        catch (FormatException exception)
        {
            throw new InvalidOperationException(
                $"Configuration value '{configurationKey}' must be a valid email address.",
                exception);
        }
    }
}

internal sealed class MailSettings
{
    public string Sender { get; init; } = string.Empty;

    public string From { get; init; } = string.Empty;

    public string To { get; init; } = string.Empty;
}

internal sealed class SmtpServerSettings
{
    public string Hostname { get; init; } = string.Empty;

    public int Port { get; init; }

    public bool EnableSsl { get; init; }

    public int TimeoutMilliseconds { get; init; }

    public string? Username { get; init; }

    public string? Password { get; init; }
}
}