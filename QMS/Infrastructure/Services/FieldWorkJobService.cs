using Application.Ports;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Application.Queries.GetFieldworkProgressByMunicipality;
using System.Text;

namespace Infrastructure.Services
{
    public class FieldWorkJobService
    {
        private readonly IFieldWorkRepository _fieldWorkRepository;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IConfiguration _configuration;
        private readonly IGetFieldworkProgressByMunicipalityQuery _getFieldworkProgressByMunicipalityQuery;
        public FieldWorkJobService(IFieldWorkRepository fieldWorkRepository,
        IEmailTemplateRepository emailTemplateRepository,
        IConfiguration configuration,
        IGetFieldworkProgressByMunicipalityQuery getFieldworkProgressByMunicipalityQuery) 
        {
            _fieldWorkRepository = fieldWorkRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _configuration = configuration;
            _getFieldworkProgressByMunicipalityQuery = getFieldworkProgressByMunicipalityQuery;
        }
        // Job open 1: Update status
        public async Task<bool> UpdateBldReviewStatusJob(int fieldWorkId, Guid updatedUser)
        {
            return await _fieldWorkRepository.UpdateBldReviewStatus(fieldWorkId, updatedUser);
        }
        // Job open 2: send emails for open fieldwork
        public async Task SendOpenEmailsJob(int fieldWorkId)
        {
            var smtpSection = _configuration.GetSection("Smtp");
            var host = smtpSection["Host"];
            var port = int.Parse(smtpSection["Port"] ?? throw new InvalidOperationException());
            var username = smtpSection["Username"];
            var encryptedPassword = smtpSection["EncryptedPassword"];
            var key = smtpSection["EncryptionKey"];
            var iv = smtpSection["EncryptionIV"];

            if (encryptedPassword != null)
            {
                if (key != null)
                {
                    if (iv != null)
                    {
                        var password = Decrypt(encryptedPassword, key, iv);

                        var fieldwork = await _fieldWorkRepository.GetFieldWork(fieldWorkId);
                        var template = await _emailTemplateRepository.GetEmailTemplate(fieldwork.OpenEmailTemplateId);
                        var users = await _fieldWorkRepository.GetActiveUsers();

                        foreach (var user in users)
                        {
                            var body = template.Body
                                .Replace("{Name}", user.Name)
                                .Replace("{Surname}", user.LastName)
                                .Replace("{StartDate}", fieldwork.StartDate.ToString("yyyy-MM-dd"))
                                .Replace("{Description}", fieldwork.Description ?? "");

                            if (host == null) continue;
                            if (username != null)
                                SendEmail(user.Email, template.Subject, body, host, port, username, password);
                        }
                    }
                }
            }
        }

        private static void SendEmail(string to, string subject, string body, string host, int port, string username, string password)
        {
            using var smtp = new SmtpClient(host, port);
            smtp.Credentials = new NetworkCredential(username, password);
            smtp.EnableSsl = true;
            var message = new MailMessage(username, to, subject, body) { IsBodyHtml = true };
            smtp.Send(message);
        }

        private static string Decrypt(string encryptedBase64, string base64Key, string base64IV)
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

        // Job close 1: Transform BldReview
        public async Task<bool> ConfirmFieldworkClosureJob(int fieldWorkId, Guid updatedUser, string remarks)
        {
            //update remarks in fieldwork
            var fieldWork = await _fieldWorkRepository.GetFieldWork(fieldWorkId);
            fieldWork.Remarks = remarks;
            await _fieldWorkRepository.UpdateFieldWork(fieldWork);
            //call the SP to transfrom BldReview status
            return await _fieldWorkRepository.TransformBldReviewForClosing(fieldWorkId, updatedUser);
        }

        // Job close 2: Send emails for closing fieldwork
        public async Task SendCloseEmailsJob(int fieldWorkId)
        {
            
            var smtpSection = _configuration.GetSection("Smtp");
            var host = smtpSection["Host"];
            var port = int.Parse(smtpSection["Port"] ?? throw new InvalidOperationException());
            var username = smtpSection["Username"];
            var encryptedPassword = smtpSection["EncryptedPassword"];
            var key = smtpSection["EncryptionKey"];
            var iv = smtpSection["EncryptionIV"];
            if (encryptedPassword != null)
            {
                if (key != null)
                {
                    if (iv != null)
                    {
                        var password = Decrypt(encryptedPassword, key, iv);

                        var fieldwork = await _fieldWorkRepository.GetFieldWork(fieldWorkId);
                        var template = await _emailTemplateRepository.GetEmailTemplate(fieldwork.CloseEmailTemplateId);
                        var users = await _fieldWorkRepository.GetActiveUsers();
                        var progress = _getFieldworkProgressByMunicipalityQuery.Execute(fieldWorkId);
                        var progressList = progress.Result.progressDTO;
                        var progressByCode = progressList
                            .GroupBy(p => p.MunicipalityCode)
                            .ToDictionary(g => g.Key, g => g.First()); // nëse ka dublikime, merr të parin

                        foreach (var user in users)
                        {
                            // Gjej progresin për bashkinë e user-it
                            FieldworkProgressByMunicipalityDTO? p = null;
                            if (TryNormalizeMunicipalityCode(user.MunicipalityCode, out var code)
                                && progressByCode.TryGetValue(code, out var found))
                            {
                                p = found;
                            }

                            // Nëse nuk gjendet progres për bashkinë e user-it, supozo 0% (ose mund të log-osh dhe të vazhdosh)
                            var progressPercent = p?.ProgressPercent ?? 0m;
                            var municipalityDisplay = p?.MunicipalityName ?? user.MunicipalityCode ?? string.Empty;

                            // Vendos suksesin sipas rregullit
                            var isCompleted = progressPercent >= 100m;

                            var body = BuildClosureBody(template.Body, isCompleted)
                                .Replace("{Name}", user.Name)
                                .Replace("{Surname}", user.LastName)
                                .Replace("{FieldworkName}", fieldwork.FieldWorkName.ToString())
                                .Replace("{Municipality}", municipalityDisplay)
                                .Replace("{ClosureDate}", DateTime.UtcNow.ToString("yyyy-MM-dd"));

                            if (host == null) continue;
                            if (username != null)
                                SendEmail(user.Email, template.Subject, body, host, port, username, password);
                        }
                    }
                }
            }
        }

        private static bool TryNormalizeMunicipalityCode(string? code, out int normalized)
        {
            normalized = 0;
            if (string.IsNullOrWhiteSpace(code)) return false;

            // Mbaj vetëm shifrat (nëse ka prefikse/mbishkrime)
            var digits = new string(code.Where(char.IsDigit).ToArray());
            return int.TryParse(digits, out normalized);
        }

        private static string BuildClosureBody(string templateBody, bool success)
        {
            // Structure:
            // {Success}...{/Success}
            // {Failed}...{/Failed}
            var successBlock = ExtractBlock(templateBody, "Success");
            var failedBlock = ExtractBlock(templateBody, "Failed");
            return success ? successBlock : failedBlock;
        }
        private static string ExtractBlock(string text, string key)
        {
            var start = text.IndexOf("{" + key + "}", StringComparison.OrdinalIgnoreCase);
            var end = text.IndexOf("{/" + key + "}", StringComparison.OrdinalIgnoreCase);
            if (start >= 0 && end > start)
            {
                return text.Substring(start + key.Length + 2, end - (start + key.Length + 2)).Trim();
            }
            return ""; // nëse mungon blloku, kthe bosh
        }

       
    }
}
