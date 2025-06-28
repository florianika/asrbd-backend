using Application.FieldWork.GetJobStatus.Request;
using Application.FieldWork.GetJobStatus.Response;
using Application.Ports;

namespace Application.FieldWork.GetJobStatus
{
    public class GetJobStatus : IGetJobStatus
    {
        private readonly IFieldWorkRepository _fieldWorkRepository;
        public GetJobStatus(IFieldWorkRepository fieldWorkRepository)
        {
            _fieldWorkRepository = fieldWorkRepository;
        }
        public async Task<GetJobStatusResponse> Execute(GetJobStatusRequest request)
        {
            var job = await _fieldWorkRepository.GetJob(request.Id);
            return new GetJobStatusSuccessResponse { Status=job.Status };

        }
    }
}
