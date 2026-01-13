
using Application.Exceptions;
using Application.FieldWork.ConfirmFieldworkClosure.Request;
using Application.FieldWork.ConfirmFieldworkClosure.Response;
using Application.FieldWork.UpdateBldReviewStatus.Response;
using Application.Ports;
using Microsoft.Extensions.Logging;

namespace Application.FieldWork.ConfirmFieldworkClosure
{
    public class ConfirmFieldworkClosure : IConfirmFieldworkClosure
    {
        private readonly ILogger _logger;
        private readonly IFieldWorkRepository _fieldWorkRepository;
        private readonly IWebSocketBroadcaster _webSocketBroadcaster;
        private readonly IJobDispatcher _jobDispatcher;

        public ConfirmFieldworkClosure(ILogger<ConfirmFieldworkClosure> logger, 
            IFieldWorkRepository fieldWorkRepository, 
            IWebSocketBroadcaster webSocketBroadcaster,
            IJobDispatcher jobDispatcher)
        {
            _logger = logger;
            _fieldWorkRepository = fieldWorkRepository;
            _webSocketBroadcaster = webSocketBroadcaster; 
            _jobDispatcher = jobDispatcher;
        }

        public async Task<ConfirmFieldworkClosureResponse> Execute(ConfirmFieldworkClosureRequest request)
        {
            try
            {
                // Pre-checks - check if fieldwork is open 
                var fieldWork = await _fieldWorkRepository.GetFieldWork(request.FieldWorkId);
                if (fieldWork.FieldWorkStatus != Domain.Enum.FieldWorkStatus.OPEN)
                    throw new AppException("Fieldwork is not OPEN");

                // Precheck - check if there are buildings with review status 3 (Review executed)
                var hasExecuted = await _fieldWorkRepository.HasBldReviewExecuted();
                if (hasExecuted)
                    throw new AppException("Closure not allowed: buildings with review status 3 (Review executed) exist.");

                //// Precheck - check if most of the buildings are tested and reviewed
                //var isMostReviewed = await _fieldWorkRepository.AreMostBuildingsReviewed();
                //if (!isMostReviewed)
                //    throw new AppException("Most buildings are not reviewed yet, cannot proceed.");

                //Broadcast closure
                await _webSocketBroadcaster.BroadcastStatusAsync(false, null, null);

                // call hangfire to transform BldReview and send emails
                _jobDispatcher.ScheduleClosureAndEmail(request.FieldWorkId, request.UpdatedUser, request.Remarks);
                return new ConfirmFieldworkClosureSuccessResponse
                {
                    Message = "Job for closing fieldwork. Emails will be sent after closure completes."
                };
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, "Fieldwork closure failed for {FieldworkId}", request.FieldWorkId);
                throw;
            }
        }
    }
}
