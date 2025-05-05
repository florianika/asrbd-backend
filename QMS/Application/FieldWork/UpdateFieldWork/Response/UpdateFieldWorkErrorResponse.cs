
namespace Application.FieldWork.UpdateFieldWork.Response
{
    public class UpdateFieldWorkErrorResponse : UpdateFieldWorkResponse
    {
        public string Message { get; internal set; }
        public string Code { get; internal set; }
    }
}
