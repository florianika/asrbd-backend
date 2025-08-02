namespace Application.FieldWork.CanBeClosed.Response
{
    public class CanBeClosedErrorResponse : CanBeClosedResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
