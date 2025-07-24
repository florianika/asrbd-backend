using System.Net.Http;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;

namespace Application.Ports
{
    public interface IWebSocketBroadcaster
    {
        Task BroadcastStatusAsync(bool isFieldworkTime, DateTime? startTime, int? fieldworkId);
        Task HandleConnection(HttpContext context, WebSocket socket);
    }
}

