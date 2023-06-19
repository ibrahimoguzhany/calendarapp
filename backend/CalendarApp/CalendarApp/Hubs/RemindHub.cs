using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CalendarApp.Hubs
{
    public class RemindHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage",message);
        }
    }
}
