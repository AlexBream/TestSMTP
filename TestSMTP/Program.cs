using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {

        var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        for (int i = 0; i < 1; i++)
        {
            SendMail(config);
        }

    }

    private static void SendMail(IConfigurationRoot config)
    {
        // Crea un nuovo oggetto MailMessage
        MailMessage mail = new MailMessage();

        // Imposta il mittente della mail
        mail.Sender = new MailAddress(config["Mail:Sender"]);
        mail.From = new MailAddress(config["Mail:From"]);

        // Aggiungi uno o più destinatari della mail
        mail.To.Add(config["Mail:To"]);

        // Imposta l'oggetto della mail
        mail.Subject = "Questa è una mail di prova";

        // Imposta il corpo della mail, che può essere in formato HTML o testo semplice
        mail.Body = $"{DateTime.Now} - Ciao, questa è una mail inviata da un programma in C#";
        mail.IsBodyHtml = false;

        // Crea un nuovo oggetto SmtpClient
        SmtpClient smtp = new SmtpClient();

        // Imposta il server SMTP che userai per inviare la mail
        smtp.Host = config["SmtpServer:Hostname"];
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

        // Imposta la porta SMTP, che di solito è la 25 o la 587
        int port = 587;
        smtp.Port = port;

        // Abilita SSL
        smtp.EnableSsl = true;
       
        // Imposta le credenziali di autenticazione, se richieste dal server SMTP
        smtp.Credentials = new NetworkCredential(config["SmtpServer:Username"], config["SmtpServer:Password"]);


        try
        {
            // Invia la mail
            smtp.Timeout = 5000;
            smtp.Send(mail);
            Console.WriteLine($"Mail Sent on port:{port}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + $" Port:{port}");
        }
    }
}