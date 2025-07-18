namespace Application.Ports
{
    public interface IFieldworkStatusNotifier
    {
        Task NotifyFieldworkStatusChanged(bool isFieldworkTime, DateTime? startTime, int? fieldworkId);
    }
}
