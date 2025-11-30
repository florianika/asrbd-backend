using Application.Building.GetBldJobStatus.Request;
using Application.Building.GetBldJobStatus.Response;
using Application.Ports;

namespace Application.Building.GetBldJobStatus
{
    public class GetBldJobStatus : IGetBldJobStatus
    {
        private readonly IBuildingRepository _buildingRepository;
        public GetBldJobStatus(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }
        public async Task<GetBldJobStatusResponse> Execute(GetBldJobStatusRequest request)
        {
            var job = await _buildingRepository.GetJobById(request.Id);
            return new GetBldJobStatusSuccessResponse { Status=job.Status };

        }
    }
}
