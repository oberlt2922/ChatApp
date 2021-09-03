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
    }
}
