
using Application.FieldWork.UpdateBldReviewStatus.Request;
using Application.FieldWork.UpdateBldReviewStatus.Response;
using Application.Ports;
using Domain.Enum;
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
                //update the BldReviewStatus to required in the geodatabase for the given fieldwork id
                var success = await _fieldWorkRepository.UpdateBldReviewStatus(request.Id, request.UpdatedUser);
                if (success)
                {
                    //if the update was successful, update the fieldwork status to OPEN
                    var fieldwork = await _fieldWorkRepository.GetFieldWorkByIdAndStatus(request.Id, FieldWorkStatus.NEW);
                    fieldwork.FieldWorkStatus = Domain.Enum.FieldWorkStatus.OPEN;
                    fieldwork.UpdatedUser = request.UpdatedUser;
                    fieldwork.UpdatedTimestamp = DateTime.Now;

                    await _fieldWorkRepository.UpdateFieldWork(fieldwork);

                    return new UpdateBldReviewStatusSuccessResponse { Message = "BldReview status set to required and fieldwork status set to OPEN" };
                }
                return new UpdateBldReviewStatusErrorResponse { Message = "There was an internal error", Code="500" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
