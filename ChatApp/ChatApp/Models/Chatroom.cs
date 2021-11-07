using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class Chatroom
    {
        public int ChatroomId { get; set; }
        [Required]
        [MaxLength(40)]
        public string ChatroomName { get; set; }
        public string AdminId { get; set; }
        [Required]
        public bool IsPublic { get; set; }

        public virtual List<AppUser> Members { get; set; }
        public virtual List<Message> Messages { get; set; }

        public Chatroom(string chatroomName, string adminId, bool isPublic, List<AppUser> members, List<Message> messages)
        {
            this.ChatroomName = chatroomName;
            this.AdminId = adminId;
            this.IsPublic = isPublic;
            this.Members = members;
            this.Messages = messages;
        }
    }
}
