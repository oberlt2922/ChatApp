using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChatApp.Models;
using Newtonsoft.Json;
using ChatApp.Data;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method is called when a user signs in
        /// This method is called for every chatroom that the user is a member of
        /// The user is added to the groups of every chatroom that they are already a member of
        /// </summary>
        public async Task AddCurrentUserToGroup(string chatroomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatroomId);
        }

        /// <summary>
        /// Called when a new chatroom is created,
        /// for each user in the new chatroom AddToNewGroup() is called which calls AddCurrentUserToNewGroup().
        /// This is because in order to add a user to a group, you have to add their ConnectionId to the group.
        /// Hub methods can only access the connection id of the current client.
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
        /// This method is called when a new chatroom is created.
        /// The current user is added to the new chatroom group, then the new chatroom is added to the chatroom list.
        /// </summary>
        public async Task AddCurrentUserToNewGroup(string chatroomId, string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatroomId);
            await Clients.User(userId).SendAsync("AddNewChatroomToList", chatroomId);
        }


        /// <summary>
        /// Removes the current user from a group when a chatroom is deleted or the user leaves a chatroom.
        /// </summary>
        /// <param name="chatroomId">The id of the group that the user is leaving</param>
        public async Task RemoveCurrentUserFromGroup(string chatroomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatroomId);
        }

        /// <summary>
        /// A method that sends a message from a user to the chatroom
        /// </summary>
        /// <param name="messageText">The text that was entered by the user</param>
        /// <param name="userId">The id of the user who sent the message</param>
        /// <param name="chatroomId">The id of the chatroom that the message is being sent to</param>
        public async Task SendMessage(string messageText, string userId, string chatroomId)
        {
            try
            {
                AppUser currentUser = await _context.AppUser.SingleOrDefaultAsync(user => user.Id == userId);
                Chatroom room = await _context.Chatroom.SingleOrDefaultAsync(room => room.ChatroomId == Convert.ToInt32(chatroomId));
                Message message = new Message();
                message.UserId = currentUser.Id;
                message.Username = currentUser.UserName;
                message.Sent = DateTime.Now;
                message.Text = messageText;
                message.ChatroomId = room.ChatroomId;
                message.Sender = currentUser;
                message.Room = room;

                _context.Message.Add(message);
                room.Messages.Add(message);
                await _context.SaveChangesAsync();

                MessageVM messagevm = new MessageVM();
                messagevm.UserId = message.UserId;
                messagevm.Username = message.Username;
                messagevm.Sent = message.Sent;
                messagevm.Text = message.Text;
                messagevm.ChatroomId = message.ChatroomId.ToString();

                await Clients.Group(room.ChatroomId.ToString()).SendAsync("ReceiveMessage", JsonConvert.SerializeObject(messagevm));
            }
            catch(Exception ex)
            {
                await Clients.User(userId).SendAsync("DisplayError", ex.Message);
            }
        }

        /// <summary>
        /// A method that is called whenever alerts need to be sent to a chatroom
        /// </summary>
        /// <param name="text">The text of the message that is being sent</param>
        /// <param name="chatroomId">The id of the chatroom that the message is being sent to</param>
        /// <param name="userId">The current user id which is used to send an error message to the client when an exception is thrown</param>
        /// <param name="newAdmin">A bool argument indicating whether admin privileges need to be granted to a new admin</param>
        public async Task SendNonUserMessage(string text, string chatroomId, string userId, bool newAdmin)
        {
            try
            {
                Chatroom room = await _context.Chatroom.SingleOrDefaultAsync(room => room.ChatroomId == Convert.ToInt32(chatroomId));
                Message message = new Message();
                message.Sent = DateTime.Now;
                message.ChatroomId = room.ChatroomId;
                message.Room = room;
                message.Text = text;

                _context.Message.Add(message);
                room.Messages.Add(message);
                await _context.SaveChangesAsync();

                MessageVM messagevm = new MessageVM();
                messagevm.Sent = message.Sent;
                messagevm.Text = message.Text;
                messagevm.ChatroomId = message.ChatroomId.ToString();

                await Clients.Group(room.ChatroomId.ToString()).SendAsync("ReceiveMessage", JsonConvert.SerializeObject(messagevm));
                if(newAdmin) await Clients.User(room.AdminId).SendAsync("GrantAdminPrivileges", room.ChatroomId);
            }
            catch (Exception ex)
            {
                await Clients.User(userId).SendAsync("DisplayError", ex.Message);
            }
        }


        /// <summary>
        /// When a chatroom is deleted the connection.on RemoveChatroom function is called on all clients
        /// </summary>
        /// <param name="chatroomId">The id of the chatroom being removed</param>
        /// <param name="userId"> the id of the current user for exception handling</param>
        public async Task RemoveChatroom(string chatroomId, string userId)
        {
            try
            {
                await Clients.Group(chatroomId).SendAsync("RemoveChatroom", chatroomId);
            }
            catch(Exception ex)
            {
                await Clients.User(userId).SendAsync("DisplayError", ex.Message);
            }
        }
    }
}