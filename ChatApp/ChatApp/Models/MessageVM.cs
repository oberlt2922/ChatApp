using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatApp.Models
{
    public class MessageVM
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("sent")]
        public DateTime Sent { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("chatroomId")]
        public string ChatroomId { get; set; }

        public MessageVM(string text, DateTime sent, string chatroomId)
        {
            this.Text = text;
            this.Sent = sent;
            this.ChatroomId = chatroomId;
        }

        public MessageVM(string text, DateTime sent, string chatroomId, string userId, string username)
        {
            this.Text = text;
            this.Sent = sent;
            this.ChatroomId = chatroomId;
            this.UserId = userId;
            this.Username = username;
        }
    }
}
