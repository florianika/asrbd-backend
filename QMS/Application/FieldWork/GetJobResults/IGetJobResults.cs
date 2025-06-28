using Application.FieldWork.GetJobResults.Request;
using Application.FieldWork.GetJobResults.Response;

namespace Application.FieldWork.GetJobResults
{
    public interface IGetJobResults : IFieldWork<GetJobResultsRequest, GetJobResultsResponse>
    {
    }
}
