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
        [Required (ErrorMessage = "Chatroom Name is required")]
        [MaxLength(40, ErrorMessage = "Chatroom Name cannot be more than 40 characters long")]
        public string ChatroomName { get; set; }
        public string AdminId { get; set; }
        [Required (ErrorMessage = "Please select either \"Public\" or \"Private\"")]
        public bool IsPublic { get; set; }

        public virtual List<AppUser> Members { get; set; }
        public virtual List<Message> Messages { get; set; }

        public Chatroom() { }

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
