using Application.Building.GetAllAnnualSnapshots.Response;
using Application.Common.Translators;
using Application.Ports;

namespace Application.Building.GetAllAnnualSnapshots
{
    public class GetAllAnnualSnapshots : IGetAllAnnualSnapshots
    {
        private readonly IBuildingRepository _buildingRepository;
        public GetAllAnnualSnapshots(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }
        public async Task<GetAllAnnualSnapshotsResponse> Execute()
        {
            var snapshots = await _buildingRepository.GetAllAnnualSnapshots();
            return new GetAllAnnualSnapshotsSuccessResponse
            {
                DownloadJobsDTO = Translator.ToDTOList(snapshots)
            };
        }
    }
}
