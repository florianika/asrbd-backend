using Application.Ports;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
    public class FieldWorkJobService
    {
        private readonly IFieldWorkRepository _fieldWorkRepository;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IConfiguration _configuration;
        public FieldWorkJobService(IFieldWorkRepository fieldWorkRepository,
        IEmailTemplateRepository emailTemplateRepository,
        IConfiguration configuration) 
        {
            _fieldWorkRepository = fieldWorkRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _configuration = configuration;
        }
        // Job 1: Update status
        public async Task<bool> UpdateBldReviewStatusJob(int fieldWorkId, Guid updatedUser)
        {
            return await _fieldWorkRepository.UpdateBldReviewStatus(fieldWorkId, updatedUser);
        }
        // Job 2: Dërgo email për çdo user
        public async Task SendEmailsJob(int fieldWorkId)
        {
            var smtpSection = _configuration.GetSection("Smtp");
            var host = smtpSection["Host"];
            var port = int.Parse(smtpSection["Port"]);
            var username = smtpSection["Username"];
            var encryptedPassword = smtpSection["EncryptedPassword"];
            var key = smtpSection["EncryptionKey"];
            var iv = smtpSection["EncryptionIV"];

            var password = Decrypt(encryptedPassword, key, iv);

            var fieldwork = await _fieldWorkRepository.GetFieldWork(fieldWorkId);
            var template = await _emailTemplateRepository.GetEmailTemplate(fieldwork.EmailTemplateId);
            var users = await _fieldWorkRepository.GetActiveUsers();

            foreach (var user in users)
            {
                var body = template.Body
                    .Replace("{Name}", user.Name)
                    .Replace("{Surname}", user.LastName)
                    .Replace("{StartDate}", fieldwork.StartDate.ToString("yyyy-MM-dd"))
                    .Replace("{Description}", fieldwork.Description ?? "");

                SendEmail(user.Email, template.Subject, body, host, port, username, password);
            }
        }

        private static void SendEmail(string to, string subject, string body, string host, int port, string username, string password)
        {
            using var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };
            var message = new MailMessage(username, to, subject, body) { IsBodyHtml = true };
            smtp.Send(message);
        }

        private string Decrypt(string encryptedBase64, string base64Key, string base64IV)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedBase64);
            var key = Convert.FromBase64String(base64Key);
            var iv = Convert.FromBase64String(base64IV);

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(encryptedBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
