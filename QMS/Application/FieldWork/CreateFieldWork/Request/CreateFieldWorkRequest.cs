
namespace Application.FieldWork.CreateFieldWork.Request
{
    public class CreateFieldWorkRequest : FieldWork.Request
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public Guid CreatedUser { get; set; }
    }    
}
