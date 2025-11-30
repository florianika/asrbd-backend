namespace Application.Building.GetBldJobStatus.Response
{
    public class GetBldJobStatusErrorResponse : GetBldJobStatusResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
