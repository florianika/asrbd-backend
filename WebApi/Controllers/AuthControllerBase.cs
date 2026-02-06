using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AuthControllerBase : ControllerBase
{
    [NonAction]
    protected string ExtractBearerToken()
    {
        return Request.Headers.Authorization.ToString().Replace("Bearer ", "");
    }
}