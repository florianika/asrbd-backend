namespace Application.FieldWork.SendFieldWorkEmail.Response
{
    public class SendFieldWorkEmailErrorResponse : SendFieldWorkEmailResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
