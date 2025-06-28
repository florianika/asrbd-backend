namespace Application.FieldWork.GetJobResults.Response
{
    public class GetJobResultsErrorResponse : GetJobResultsResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
