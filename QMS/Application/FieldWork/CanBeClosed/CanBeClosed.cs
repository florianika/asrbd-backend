using Application.Exceptions;
using Application.FieldWork.CanBeClosed.Request;
using Application.FieldWork.CanBeClosed.Response;
using Application.FieldWork.ExecuteJob.Response;
using Application.Ports;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Application.FieldWork.CanBeClosed
{
    public class CanBeClosed : ICanBeClosed
    {
        private readonly ILogger _logger;
        private readonly IFieldWorkRepository _fieldWorkRepository;

        public CanBeClosed(ILogger<CanBeClosed> logger, IFieldWorkRepository fieldWorkRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fieldWorkRepository = fieldWorkRepository ?? throw new ArgumentNullException(nameof(fieldWorkRepository));
        }

        public async Task<CanBeClosedResponse> Execute(CanBeClosedRequest request)
        {
            try
            {
                var fieldWork = await _fieldWorkRepository.GetFieldWork(request.Id);
                if (fieldWork.FieldWorkStatus == Domain.Enum.FieldWorkStatus.OPEN)
                {

                    var result = await _fieldWorkRepository.HasBldReviewExecuted();
                    if(result) //check in the database if there are buildings with BldReview=3 --Review executed
                    {
                        return new CanBeClosedSuccessResponse
                        {
                            FieldWorkId = fieldWork.FieldWorkId,
                            CanBeClosed = false,
                            Reasons = "Supervisor approval incomplete",
                            LastChecked = DateTime.Now
                        };
                    }
                    else
                    {
                        return new CanBeClosedSuccessResponse
                        {
                            FieldWorkId = fieldWork.FieldWorkId,
                            CanBeClosed = true,
                            Reasons = "",
                            LastChecked = DateTime.Now
                        };
                    }
                }
                else
                {
                    return new CanBeClosedSuccessResponse
                    {
                        FieldWorkId = fieldWork.FieldWorkId,
                        CanBeClosed = false,
                        Reasons = "Field work is not open.",
                        LastChecked = DateTime.Now
                    };
                }
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

    }
}
