
using Application.FieldWork.UpdateBldReviewStatus.Request;
using Application.FieldWork.UpdateBldReviewStatus.Response;
using Application.Ports;
using Microsoft.Extensions.Logging;

namespace Application.FieldWork.UpdateBldReviewStatus
{
    public class UpdateBldReviewStatus : IUpdateBldReviewStatus
    {
        private readonly ILogger _logger;
        private readonly IFieldWorkRepository _fieldWorkRepository;
        public UpdateBldReviewStatus(ILogger<UpdateBldReviewStatus> logger, IFieldWorkRepository fieldWorkRepository)
        {
            _logger = logger;
            _fieldWorkRepository = fieldWorkRepository;
        }
        public async Task<UpdateBldReviewStatusResponse> Execute(UpdateBldReviewStatusRequest request)
        {
            try
            {
                var success = await _fieldWorkRepository.UpdateBldReviewStatus(request.Id, request.UpdatedUser);
                if (success)
                {
                    return new UpdateBldReviewStatusSuccessResponse { Message = "BldReview status set to required" };
                }
                return new UpdateBldReviewStatusErrorResponse { Message = "There was an internal error", Code="500" };
            }
            catch (Exception ex)
            {
                return new UpdateBldReviewStatusErrorResponse
                { Message = "There was an error", Code = ex.GetType().Name };
            }
        }
    }
}
