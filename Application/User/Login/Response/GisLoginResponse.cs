namespace Application.User.Login.Response;

public class GisLoginResponse
{
    public string Token { get; set; }
    public long Expires { get; set; }
    public bool Ssl { get; set; }
}