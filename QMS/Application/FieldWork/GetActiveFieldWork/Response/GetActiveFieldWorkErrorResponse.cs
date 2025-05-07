
namespace Application.FieldWork.GetActiveFieldWork.Response
{
    public class GetActiveFieldWorkErrorResponse : GetActiveFieldWorkResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
