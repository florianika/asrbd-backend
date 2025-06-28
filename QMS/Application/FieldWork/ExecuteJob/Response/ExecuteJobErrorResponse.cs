namespace Application.FieldWork.ExecuteJob.Response
{
    public class ExecuteJobErrorResponse : ExecuteJobResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
