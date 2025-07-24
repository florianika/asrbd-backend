
using Application.Exceptions;
using Application.FieldWork.UpdateBldReviewStatus.Request;
using Application.FieldWork.UpdateBldReviewStatus.Response;
using Application.Ports;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.FieldWork.UpdateBldReviewStatus
{
    public class UpdateBldReviewStatus : IUpdateBldReviewStatus
    {
        private readonly ILogger _logger;
        private readonly IFieldWorkRepository _fieldWorkRepository;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IJobDispatcher _jobDispatcher;
        private readonly IWebSocketBroadcaster _webSocketBroadcaster;

        public UpdateBldReviewStatus(ILogger<UpdateBldReviewStatus> logger, IFieldWorkRepository fieldWorkRepository, IServiceScopeFactory serviceScopeFactory, 
            IConfiguration configuration, IJobDispatcher jobDispatcher, IWebSocketBroadcaster webSocketBroadcaster)
        {
            _logger = logger;
            _fieldWorkRepository = fieldWorkRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
            _jobDispatcher = jobDispatcher;
            _webSocketBroadcaster = webSocketBroadcaster;
        }
        public async Task<UpdateBldReviewStatusResponse> Execute(UpdateBldReviewStatusRequest request)
        {
            try
            {
                var fieldwork = await _fieldWorkRepository.GetFieldWork(request.Id);
                //broadcast open status
                await _webSocketBroadcaster.BroadcastStatusAsync(
                    true,
                    fieldwork.StartDate,
                    fieldwork.FieldWorkId
                );
                _jobDispatcher.ScheduleBldReviewAndEmail(request.Id, request.UpdatedUser);
                return new UpdateBldReviewStatusSuccessResponse
                {
                    Message = "Job for updating status queued. Emails will be sent after update completes."
                };
            }
            catch (AppException appEx)
            {
                //broadcast status that there is no active fieldwork
                await _webSocketBroadcaster.BroadcastStatusAsync(false,null,null);
                _logger.LogError(appEx,"An error occurred");
                throw new AppException(appEx.Message);
            }
        }


    }
}
