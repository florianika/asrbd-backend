using Application.Common.Translators;
using Application.Exceptions;
using Application.FieldWork.GetJobResults.Request;
using Application.FieldWork.GetJobResults.Response;
using Application.Ports;
using Microsoft.Extensions.Logging;

namespace Application.FieldWork.GetJobResults
{
    public class GetJobResults : IGetJobResults
    {
        private readonly IFieldWorkRepository _fieldWorkRepository;
        private ILogger _logger;
        public GetJobResults(IFieldWorkRepository fieldWorkRepository, ILogger<GetJobResults> logger)
        {
            _fieldWorkRepository = fieldWorkRepository;
            _logger = logger;
        }
        public async Task<GetJobResultsResponse> Execute(GetJobResultsRequest request)
        {
            var job = await _fieldWorkRepository.GetJobById(request.Id);
            if (job.Status != Domain.Enum.JobStatus.COMPLETED)
                throw new AppException("Job is not completed yet");
            var statistics = await _fieldWorkRepository.GetStatistics(request.Id);
            return new GetJobResultsSuccessResponse
            {
                StatisticsDTO = Translator.ToDTOList(statistics)
            };
        }
    }
}
