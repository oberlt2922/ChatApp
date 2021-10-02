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
        public async Task AddCurrentUserToNewGroup(string chatroomId, string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatroomId);
            await Clients.User(userId).SendAsync("AddNewChatroomToList", chatroomId);
        }

        //Add message to chatroom and send it to group
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

        //connection.on function
        public async Task LeftChatroomMessage(string chatroomId, string username)
        {
            Chatroom room = await _context.Chatroom.SingleOrDefaultAsync(room => room.ChatroomId == Convert.ToInt32(chatroomId));
            Message message = new Message();
            message.Sent = DateTime.Now;
            message.Text = $"{username} left the chatroom.";
            message.ChatroomId = room.ChatroomId;
            message.Room = room;

            _context.Message.Add(message);
            room.Messages.Add(message);
            await _context.SaveChangesAsync();
            await Clients.Group(room.ChatroomId.ToString()).SendAsync("ReceiveMessage", JsonConvert.SerializeObject(message));
        }

        //connection.on function
        public async Task NewAdminMessage(string chatroomId, string adminId)
        {
            string newAdminUsername = _context.AppUser
                .Where(User => User.Id == adminId)
                .Select(User => User.UserName)
                .Single();
            Chatroom room = await _context.Chatroom.SingleOrDefaultAsync(room => room.ChatroomId == Convert.ToInt32(chatroomId));
            Message message = new Message();
            message.Sent = DateTime.Now;
            message.Text = $"{newAdminUsername} has been assigned as the chatroom admin.";
            message.ChatroomId = room.ChatroomId;
            message.Room = room;

            _context.Message.Add(message);
            room.Messages.Add(message);
            await _context.SaveChangesAsync();
            await Clients.Group(room.ChatroomId.ToString()).SendAsync("ReceiveMessage", JsonConvert.SerializeObject(message));
            await Clients.User(room.AdminId).SendAsync("GrantAdminPrivileges", room.ChatroomId);
        }
    }
}