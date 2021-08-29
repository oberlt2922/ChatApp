using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChatApp.Models;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        //collection of online users key is userid value is connectionid
        Dictionary<string, string> ConnectedIds = new Dictionary<string, string>();

        public void AddConnection(string userId)
        {
            ConnectedIds.Add(userId, Context.ConnectionId);
        }

        public async Task AddCurrentUserToGroup(string chatroomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatroomId);
        }

        public async Task AddMembersToGroup(string chatroomId, AppUser[] users)
        {
            string value;
            foreach(AppUser user in users)
            {
                if(ConnectedIds.TryGetValue(user.Id, out value))
                {
                    await Groups.AddToGroupAsync(value, chatroomId);
                }
            }
            await Clients.Group(chatroomId).SendAsync("AddNewChatroom", chatroomId);
        }
    }
}