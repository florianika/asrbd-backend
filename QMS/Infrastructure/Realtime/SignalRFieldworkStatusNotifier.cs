using Application.Ports;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Realtime
{
    public class SignalRFieldworkStatusNotifier : IFieldworkStatusNotifier
    {
        private readonly IHubContext<Hub> _hubContext;
        public SignalRFieldworkStatusNotifier(IHubContext<Hub> hubContext)
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
