namespace Application.ProcessOutputLog.ResolveProcessOutputLog.Response;

public class ResolveProcessOutputLogErrorResponse : ResolveProcessOutputLogResponse
{
    public string? Message { get; set; }
    public string? Code { get; set; }
}