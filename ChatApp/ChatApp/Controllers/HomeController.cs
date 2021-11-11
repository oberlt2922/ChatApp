using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //If user is signed in, the user, chatrooms, and messages are fetched and returned to the view.
        public async Task<IActionResult> Index()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return View();
            }
            else
            {
                AppUser currentUser = await _userManager.GetUserAsync(User);
                currentUser = await _context.AppUser
                        .Include(AppUser => AppUser.Chatrooms)
                            .ThenInclude(Chatroom => Chatroom.Messages
                                .OrderBy(Messages => Messages.Sent))
                        .Include(AppUser => AppUser.Chatrooms)
                    .SingleAsync(AppUser => AppUser.Id == currentUser.Id);
                return View(currentUser);
            }
        }

        /// <summary>
        /// Method to create chatroom.
        /// Checks if users exist before adding them to the chatroom.
        /// </summary>
        /// <param name="isPublic">If the chatroom is public or private</param>
        /// <param name="chatroomName">The name of the chatroom</param>
        /// <param name="usernames">An array of the usernames that the creator of the chatroom entered</param>
        /// <returns>Returns the created chatroom as json object</returns>
        [HttpPost]
        public async Task<JsonResult> CreateChatroom(string isPublic, string chatroomName, string[] usernames)
        {
            AppUser currentUser = await _userManager.GetUserAsync(User);
            Chatroom room = new Chatroom(chatroomName, currentUser.Id, isPublic == "Public" ? true: false, new List<AppUser>(), new List<Message>());
            room.Members.Add(currentUser);
            foreach (string user in usernames)
            {
                AppUser member = await _context.AppUser.SingleOrDefaultAsync(a => a.UserName == user);
                if (member != null)
                {
                    room.Members.Add(member);
                }
            }
            _context.Chatroom.Add(room);
            _context.SaveChanges();
            return GetChatroom(room.ChatroomId.ToString());
        }

        /// <summary>
        /// Adds members to a chatroom
        /// </summary>
        /// <param name="chatroomId">The id of the chatroom that the members are being added to</param>
        /// <param name="members">The usernames of the members being added to the chatroom</param>
        /// <returns>Returns the chatroom id and the new members' ids as json object</returns>
        [HttpPost]
        public async Task<JsonResult> AddMembers(string chatroomId, string[] usernames)
        {
            Chatroom room = _context.Chatroom.Where(c => c.ChatroomId == Convert.ToInt32(chatroomId))
                .Include(c => c.Members).Single();
            var returnObject = new { chatroomId = chatroomId, members = new List<AppUser>() };
            foreach (string user in usernames)
            {
                AppUser member = await _context.AppUser.SingleOrDefaultAsync(a => a.UserName == user);
                if (member != null)
                {
                    returnObject.members.Add(member);
                    room.Members.Add(member);
                }
            }
            _context.SaveChanges();
            return Json(returnObject);
        }

        //Fetches and returns the chatroom as Json
        [HttpPost]
        public JsonResult GetChatroom(string chatroomId)
        {
            Chatroom chatroom = _context.Chatroom
                .Include(Chatroom => Chatroom.Messages.OrderBy(Message => Message.Sent))
                .Include(Chatroom => Chatroom.Members)
                .Single(Chatroom => Chatroom.ChatroomId == Convert.ToInt32(chatroomId));
            return Json(chatroom);
        }

        //get chatrooms by prefix where chatroom is public and user is not already a member
        [HttpPost]
        public async Task<JsonResult> AutoCompleteChatroom(string prefix, string userId)
        {
            List<Chatroom> chatrooms = await _context.Chatroom
                .Include(Chatroom => Chatroom.Members)
                .Where(Chatroom => Chatroom.ChatroomName.StartsWith(prefix) 
                    && Chatroom.IsPublic == true 
                    && !(Chatroom.Members.Any(m => m.Id == userId) || (_context.BlockedUsers.Any(b => b.UserId == userId && b.ChatroomId == Chatroom.ChatroomId))))
                .ToListAsync();
            return Json(chatrooms);
        }

        //join chatroom accept chatroom id and user id return chatroom json result
        public async Task<JsonResult> JoinChatroom(string chatroomId)
        {
            Chatroom chatroom = _context.Chatroom
                .Include(Chatroom => Chatroom.Messages.OrderBy(Message => Message.Sent))
                .Include(Chatroom => Chatroom.Members)
                .Single(Chatroom => Chatroom.ChatroomId == Convert.ToInt32(chatroomId));
            AppUser currentUser = await _userManager.GetUserAsync(User);
            chatroom.Members.Add(currentUser);
            _context.SaveChanges();
            return Json(chatroom);
        }

        //leave chatroom and choose new admin if current user is admin
        public async Task<JsonResult> LeaveChatroom(string chatroomId)
        {
            Chatroom chatroom = await _context.Chatroom
                .Include(Chatroom => Chatroom.Members)
                .SingleAsync(Chatroom => Chatroom.ChatroomId == Convert.ToInt32(chatroomId));
            AppUser currentUser = await _userManager.GetUserAsync (User);
            bool adminChanged = false;
            string adminUsername = "";
            if(chatroom.Members.Count == 1)
            {
                DeleteChatroom(chatroom.ChatroomId);
                //return Json(new {adminChanged = "", adminId = ""});
                return Json(new { deleted = true });
            }
            if(chatroom.AdminId == currentUser.Id)
            {
                adminChanged = true;
                var newAdmin = await _context.Message.
                    Where(m => m.UserId != null && m.ChatroomId == Convert.ToInt32(chatroomId) && chatroom.Members.Contains(m.Sender) && m.UserId != chatroom.AdminId).
                    GroupBy(m => m.UserId).
                    Select(g => new { UserId = g.Key, count = g.Count() }).
                    OrderByDescending(g => g.count).
                    FirstAsync();
                chatroom.AdminId = newAdmin.UserId;
                adminUsername = _context.AppUser.Where(a => a.Id == newAdmin.UserId).Select(u => u.UserName).Single();
            }
            var returnObject = new { adminChanged = adminChanged, adminId = chatroom.AdminId, adminUsername = adminUsername };
            chatroom.Members.Remove(currentUser);
            await _context.SaveChangesAsync();
            return Json(returnObject);
        }

        //delete chatroom
        public void DeleteChatroom(int chatroomId)
        {
            Chatroom chatroom = _context.Chatroom.Include(c => c.Messages).Single(c => c.ChatroomId == chatroomId);
            _context.Remove(chatroom);
            _context.SaveChanges();
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
