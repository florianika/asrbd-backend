
namespace Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId.Request
{
    public class GetProcessOutputLogsByEntranceIdRequest : ProcessOutputLog.Request
    {
        public Guid EntId { get; set; }
    }
}
