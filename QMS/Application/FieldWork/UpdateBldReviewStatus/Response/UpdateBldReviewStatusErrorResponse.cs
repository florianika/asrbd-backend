namespace Application.FieldWork.UpdateBldReviewStatus.Response
{
    public class UpdateBldReviewStatusErrorResponse : UpdateBldReviewStatusResponse
    {
        public required string Message { get; set; }
        public required string Code { get; set; }
    }
}
