using Application.Building.GetRunningJobs.Response;
using Application.Common.Translators;
using Application.Ports;
using Hangfire.Storage.Monitoring;

namespace Application.Building.GetRunningJobs
{
    public class GetRunningJobs : IGetRunningJobs
    {
        private readonly IBuildingRepository _buildingRepository;
        public GetRunningJobs(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }
        public async Task<GetRunningJobsSuccessResponse> Execute()
        {
            var jobs = await _buildingRepository.GetRunningJobs();
            return new GetRunningJobsSuccessResponse
            {
                JobsDTO = Translator.ToDTOList(jobs)
            };
        }
    }
}
