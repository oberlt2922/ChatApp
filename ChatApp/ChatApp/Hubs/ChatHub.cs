using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChatApp.Models;
using Newtonsoft.Json;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        /// <summary>
        /// This method is called when a user signs in
        /// The user is added to the groups of every chatroom that they are already a member of
        /// </summary>
        public async Task AddCurrentUserToGroup(string chatroomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatroomId);
        }

        /// <summary>
        /// called when a new chatroom is created
        /// for each user in the new chatroom the AddCurrentUserToNewGroup function is called which adds that user to the group
        /// after the user is added to the group the chatroom is displayed
        /// </summary>
        public async Task AddMembersToGroup(string chatroomId, string users)
        {
            List<AppUser> members = JsonConvert.DeserializeObject<List<AppUser>>(users);
            foreach (AppUser user in members)
            {
                await Clients.User(user.Id).SendAsync("AddToNewGroup", chatroomId);
            }
        }

        /// <summary>
        /// This method is called when a new chatroom is created
        /// The current user is added to the new chatroom group, then the new chatroom is displayed in the chatroom list
        /// </summary>
        /// <param name="chatroomId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task AddCurrentUserToNewGroup(string chatroomId, string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatroomId);
            await Clients.User(userId).SendAsync("AddNewChatroomToList", chatroomId);
        }
    }
}