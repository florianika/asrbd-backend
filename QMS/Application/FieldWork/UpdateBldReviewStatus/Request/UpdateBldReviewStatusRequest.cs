namespace Application.FieldWork.UpdateBldReviewStatus.Request
{
    public class UpdateBldReviewStatusRequest : FieldWork.Request
    {
        public int Id { get; set; } //The Id of the fieldworks that contains the selected rules
        public Guid UpdatedUser { get; set; }
    }
}
