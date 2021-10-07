using LuckyDucky.MailService.Interfaces;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace LuckyDucky.MailService {
    public class EmailService : IMailService {

        private readonly EmailSettingsModel _emailSettings;

        public EmailService(EmailSettingsModel model)
        {
            _emailSettings = model;
        }

        private async Task SenderAsync(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
                await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, false);
                await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        public async Task SendHtmlEmail(string[] recipients, string title, string htmlBody)
        {

            foreach (var email in recipients)
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = title;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = htmlBody
                };

                await SenderAsync(emailMessage);
            }
        }

        public async Task SendPlainTextEmail(string[] recipients, string title, string plainTextBody)
        {

            foreach (var email in recipients)
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = title;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
                {
                    Text = plainTextBody
                };

                await SenderAsync(emailMessage);
            }
        }
    }
}