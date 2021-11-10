using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class BlockedUsers
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ChatroomId { get; set; }

        public BlockedUsers() { }

        public BlockedUsers(string userId, int chatroomId)
        {
            this.UserId = userId;
            this.ChatroomId = chatroomId;
        }
    }
}
