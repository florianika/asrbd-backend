namespace Application.User.Login.Request;

public class GisLoginRequest
{
    public required Dictionary<string, string> Form { get; set; }
}