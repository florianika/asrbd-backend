using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public abstract class QmsControllerBase : ControllerBase
{
    public string ExtractBearerToken()
    {
        return Request.Headers.Authorization.ToString().Replace("Bearer ", "");
    }

}