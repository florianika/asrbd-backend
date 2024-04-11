namespace Application.ProcessOutputLog.ResolveProcessOutputLog.Response;

public abstract class ResolveProcessOutputLogResponse : ProcessOutputLog.Response
{
    public Guid ProcessOutputLogId { get; set; }
}