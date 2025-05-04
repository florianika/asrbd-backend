namespace Application.FieldWork.CreateFieldWork.Response
{
    public class CreateFieldWorkErrorResponse : CreateFieldWorkResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
