using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Messages = new HashSet<Message>();
            Chatrooms = new HashSet<Chatroom>();
        }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Chatroom> Chatrooms { get; set; }
    }
}
