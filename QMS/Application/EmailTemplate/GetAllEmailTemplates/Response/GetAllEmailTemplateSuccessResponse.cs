using Application.EmailTemplate.CreateEmailTemplate.Response;

namespace Application.EmailTemplate.GetAllEmailTemplate.Response
{
    public class GetAllEmailTemplateSuccessResponse : GetAllEmailTemplateResponse
    {
        public IEnumerable<EmailTemplateDTO> EmailTemplateDTOs { get; set; } = new List<EmailTemplateDTO>();
    }
}
