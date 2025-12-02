using Application.Building.GetAnnualSnapshotById.Request;
using Application.Building.GetAnnualSnapshotById.Response;
using Application.Common.Translators;
using Application.Ports;

namespace Application.Building.GetAnnualSnapshotById
{
    public class GetAnnualSnapshotById : IGetAnnualSnapshotById
    {
        private readonly IBuildingRepository _buildingRepository;
        public GetAnnualSnapshotById(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        public async Task<GetAnnualSnapshotByIdResponse> Execute(GetAnnualSnapshotByIdRequest request)
        {
            var downloadJob = await _buildingRepository.GetAnnualSnapshotById(request.Id);
            var downloadJobDto = Translator.ToDTO(downloadJob);
            return new GetAnnualSnapshotByIdSuccessResponse
            {
                DownloadJobDTO = downloadJobDto
            };
        }
    }
}
