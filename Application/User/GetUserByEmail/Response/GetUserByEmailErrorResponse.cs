namespace Application.User.GetUserByEmail.Response;

public class GetUserByEmailErrorResponse : GetUserByEmailResponse
{
    public string? Message { get; set; }
    public string? Code { get; set; }
}