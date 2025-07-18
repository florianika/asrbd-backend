using Application.Ports;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    //[Authorize]
    public class FieldworkHub : Hub
    {

        private readonly IFieldWorkRepository _fieldWorkRepository;
        public FieldworkHub(IFieldWorkRepository fieldWorkRepositoryy)
        {
            _fieldWorkRepository = fieldWorkRepositoryy;
        }
        public override async Task OnConnectedAsync()
        {
            var fieldwork = await _fieldWorkRepository.GetCurrentOpenFieldwork();

            if (fieldwork != null)
            {
                await Clients.Caller.SendAsync("FieldworkStatusChanged", new
                {
                    isFieldworkTime = true,
                    startTime = fieldwork.StartDate,
                    fieldworkId = fieldwork.FieldWorkId
                });
            }
            else
            {
                await Clients.Caller.SendAsync("FieldworkStatusChanged", new
                {
                    isFieldworkTime = false,
                    startTime = (DateTime?)null,
                    fieldworkId = (Guid?)null
                });
            }

            await base.OnConnectedAsync();
        }
    }
}
