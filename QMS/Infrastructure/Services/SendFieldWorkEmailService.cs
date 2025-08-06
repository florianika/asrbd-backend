using Application.FieldWork.SendFieldWorkEmail;
using Application.FieldWork.SendFieldWorkEmail.Request;
using Application.FieldWork.SendFieldWorkEmail.Response;
using Application.Ports;
using Hangfire;
using Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
    public class SendFieldWorkEmailService : ISendFieldWorkEmail
    {
        private readonly DataContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;
        public SendFieldWorkEmailService(DataContext dataContext, IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _context = dataContext;
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
        }
        public async Task<SendFieldWorkEmailResponse> Execute(SendFieldWorkEmailRequest request)
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var fieldWorkRepository = scope.ServiceProvider.GetRequiredService<IFieldWorkRepository>();
                var emailTemplateRepository = scope.ServiceProvider.GetRequiredService<IEmailTemplateRepository>();

                // Read SMTP settings
                var smtpSection = _configuration.GetSection("Smtp");
                var host = smtpSection["Host"];
                var portValue = smtpSection["Port"];
                var username = smtpSection["Username"];
                var encryptedPassword = smtpSection["EncryptedPassword"];
                var key = smtpSection["EncryptionKey"];
                var iv = smtpSection["EncryptionIV"];

                if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(portValue) || string.IsNullOrEmpty(username)
                    || string.IsNullOrEmpty(encryptedPassword) || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(iv))
                {
                    throw new InvalidOperationException("SMTP configuration is missing or incomplete.");
                }

                var password = Decrypt(encryptedPassword, key, iv);
                var port = int.Parse(portValue);

                var fieldwork = await fieldWorkRepository.GetFieldWork(request.FieldWorkId);
                var template = await emailTemplateRepository.GetEmailTemplate(fieldwork.OpenEmailTemplateId);
                var users = await fieldWorkRepository.GetActiveUsers();

                foreach (var user in users)
                {
                    var body = template.Body
                        .Replace("{Name}", user.Name)
                        .Replace("{Surname}", user.LastName)
                        .Replace("{StartDate}", fieldwork.StartDate.ToString("yyyy-MM-dd"))
                        .Replace("{Description}", fieldwork.Description ?? "");

                    BackgroundJob.Enqueue(() =>
                        SendEmail(user.Email, template.Subject, body, host, port, username, password)
                    );
                }

                return new SendFieldWorkEmailSuccessResponse
                {
                    Message = "FieldWork emails queued successfully"
                };
            }
            catch (Exception ex)
            {
                return new SendFieldWorkEmailErrorResponse
                {
                    Message = "Failed to send FieldWork emails",
                    Code = ex.Message
                };
            }
        }

        private static void SendEmail(string toEmail, string subject, string body, string host, int port, string username, string password)
        {
            using var client = new SmtpClient(host, port);
            client.Credentials = new NetworkCredential(username, password);
            client.EnableSsl = true;

            var mail = new MailMessage(username, toEmail, subject, body)
            {
                IsBodyHtml = true
            };

            client.Send(mail);
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

            /*
            public static string Encrypt(string plainText, string key, string iv)
            {
                using var aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(iv);

                using var ms = new MemoryStream();
                using var crypto = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
                using (var writer = new StreamWriter(crypto))
                {
                    writer.Write(plainText);
                }

                var encryptedBytes = ms.ToArray();
                return Encoding.UTF8.GetString(encryptedBytes);
            }*/
        }
}
