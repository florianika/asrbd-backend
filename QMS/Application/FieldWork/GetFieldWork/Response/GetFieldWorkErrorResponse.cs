namespace Application.FieldWork.GetFieldWork.Response
{
    public class GetFieldWorkErrorResponse : GetFieldWorkResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
