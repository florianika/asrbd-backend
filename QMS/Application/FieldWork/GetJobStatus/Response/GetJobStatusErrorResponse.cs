namespace Application.FieldWork.GetJobStatus.Response
{
    public class GetJobStatusErrorResponse : GetJobStatusResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
