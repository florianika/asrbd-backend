namespace Application.Ports
{
    public interface INotificationSender
    {
        Task SendEmailAsync(string to, string subject, string htmlBody);
    }
}
