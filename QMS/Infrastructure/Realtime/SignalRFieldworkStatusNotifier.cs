using Application.Ports;
using Infrastructure.Realtime.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Realtime
{
    public class SignalRFieldworkStatusNotifier : IFieldworkStatusNotifier
    {
        private readonly IHubContext<FieldworkHub> _hubContext;
        public SignalRFieldworkStatusNotifier(IHubContext<FieldworkHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task NotifyFieldworkStatusChanged(bool isFieldworkTime, DateTime? startTime, int? fieldworkId)
        {          
            await _hubContext.Clients.All.SendAsync("FieldworkStatusChanged", new
            {
                isFieldworkTime = true,
                startTime = DateTime.UtcNow,
                fieldworkId = fieldworkId
            });

        }
    }
}
