using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace QRClipboard.Hubs
{
    public class ClipboardHub : Hub
    {
        public async Task SendMessage(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }

        public Task JoinGroup(string name)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, name);
        }
    }
}