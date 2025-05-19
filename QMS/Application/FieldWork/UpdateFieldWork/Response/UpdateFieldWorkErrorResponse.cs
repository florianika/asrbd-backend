
namespace Application.FieldWork.UpdateFieldWork.Response
{
    public class UpdateFieldWorkErrorResponse : UpdateFieldWorkResponse
    {
        public required string Message { get; set; }
        public required string Code { get; set; }
    }
}
