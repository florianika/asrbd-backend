using Application.Ports;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;

namespace Infrastructure.Repositories
{
    public class SmtpEmailSender : INotificationSender
    {
        private readonly IConfiguration _cfg;
        public SmtpEmailSender(IConfiguration cfg) => _cfg = cfg;
        public async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
            var smtp = _cfg.GetSection("Smtp");
            var host = smtp["Host"];
            var port = int.Parse(smtp["Port"] ?? "587");
            var username = smtp["Username"];
            var encPass = smtp["EncryptedPassword"];
            var key = Convert.FromBase64String(smtp["EncryptionKey"]!);
            var iv = Convert.FromBase64String(smtp["EncryptionIV"]!);

            string Decrypt(string encryptedBase64)
            {
                var data = Convert.FromBase64String(encryptedBase64);
                using var aes = Aes.Create();
                aes.Key = key; aes.IV = iv; aes.Mode = CipherMode.CBC; aes.Padding = PaddingMode.PKCS7;
                using var dec = aes.CreateDecryptor();
                var plain = dec.TransformFinalBlock(data, 0, data.Length);
                return System.Text.Encoding.UTF8.GetString(plain);
            }

            using var client = new SmtpClient(host!, port)
            {
                Credentials = new NetworkCredential(username, Decrypt(encPass!)),
                EnableSsl = true
            };
            using var msg = new MailMessage(username!, to, subject, htmlBody) { IsBodyHtml = true };
            await client.SendMailAsync(msg);
        }
    }
}
