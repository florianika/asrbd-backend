
namespace Application.FieldWork.AssociateEmailTemplateWithFieldWork.Response
{
    public class AssociateEmailTemplateWithFieldWorkErrorResponse : AssociateEmailTemplateWithFieldWorkResponse
    {
        public required string Message { get; set; }
        public required string Code { get; set; }
    }
}
