namespace Application.FieldWork.ExecuteJob.Request
{
    public class ExecuteJobRequest : FieldWork.Request
    {
        public int Id { get; set; } //fieldworkId
        public Guid CreatedUser { get; set; }
    }
}
