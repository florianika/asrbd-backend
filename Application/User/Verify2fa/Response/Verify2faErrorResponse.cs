namespace Application.User.Verify2fa.Response
{
    public class Verify2faErrorResponse : Verify2faResponse
    {
        public string? Message { get; internal set; }
        public string? Code { get; internal set; }
    }
}
