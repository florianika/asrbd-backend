using Application.FieldWork.GetJobStatus.Request;
using Application.FieldWork.GetJobStatus.Response;

namespace Application.FieldWork.GetJobStatus
{
    public interface IGetJobStatus : IFieldWork<GetJobStatusRequest,GetJobStatusResponse>
    {
    }
}
