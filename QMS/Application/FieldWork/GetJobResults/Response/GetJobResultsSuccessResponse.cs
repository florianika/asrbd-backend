using Application.Note;
using Domain;

namespace Application.FieldWork.GetJobResults.Response
{
    public class GetJobResultsSuccessResponse : GetJobResultsResponse
    {
        public required IEnumerable<StatisticsDTO> StatisticsDTO { get; set; }
    }
}
