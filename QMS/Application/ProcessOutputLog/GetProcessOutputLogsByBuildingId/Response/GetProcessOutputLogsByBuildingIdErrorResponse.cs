
namespace Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Response
{
    public class GetProcessOutputLogsByBuildingIdErrorResponse : GetProcessOutputLogsByBuildingIdResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
