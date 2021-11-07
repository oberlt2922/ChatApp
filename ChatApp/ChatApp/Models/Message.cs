using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Username { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Sent { get; set; }

        public string UserId { get; set; }
        public virtual AppUser Sender { get; set; }

        [Required]
        public int ChatroomId { get; set; }
        public virtual Chatroom Room { get; set; }

        public Message(string text, DateTime sent, Chatroom room)
        {
            this.Text = text;
            this.Sent = sent;
            this.ChatroomId = room.ChatroomId;
            this.Room = room;
        }

        public Message(string text, DateTime sent, AppUser sender, Chatroom room)
        {
            this.Text = text;
            this.Sent = sent;
            this.Username = sender.UserName;
            this.UserId = sender.Id;
            this.Sender = sender;
            this.ChatroomId = room.ChatroomId;
            this.Room = room;
        }
    }
}
