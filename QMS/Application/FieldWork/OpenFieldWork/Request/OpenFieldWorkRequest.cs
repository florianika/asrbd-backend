
namespace Application.FieldWork.OpenFieldWork.Request
{
    public class OpenFieldWorkRequest : FieldWork.Request
    {
        public int FieldWorkId { get; set; }
        public Guid UpdatedUser { get; set; }
    }
}
