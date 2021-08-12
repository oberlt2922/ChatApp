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
        [Required]
        public string Username { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Sent { get; set; }

        public string UserId { get; set; }
        public virtual AppUser Sender { get; set; }

        [Required]
        public int ChatroomId { get; set; }
        public virtual Chatroom Room { get; set; }
    }
}
