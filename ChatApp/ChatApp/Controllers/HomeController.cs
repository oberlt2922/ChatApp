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

        [HttpPost]
        public async Task<JsonResult> CreateChatroom(string isPublic, string chatroomName, string[] username)
        {
            Chatroom room = new Chatroom();
            AppUser currentUser = await _userManager.GetUserAsync(User);
            room.AdminId = currentUser.Id;
            room.ChatroomName = chatroomName;
            room.IsPublic = (isPublic == "Public") ? true : false;
            room.Members = new List<AppUser>();
            room.Messages = new List<Message>();
            room.Members.Add(currentUser);
            foreach (string user in username)
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
            List<Chatroom> returnList = new List<Chatroom>();
            List<Chatroom> chatrooms = await _context.Chatroom
                .Include(Chatroom => Chatroom.Members)
                .Where(Chatroom => Chatroom.ChatroomName.StartsWith(prefix) && Chatroom.IsPublic == true)
                .ToListAsync();
            foreach(Chatroom chatroom in chatrooms)
            {
                bool userIsMember = false;
                foreach(AppUser member in chatroom.Members)
                {
                    if(member.Id == userId)
                    {
                        userIsMember = true;
                        break;
                    }
                }
                if (userIsMember == false)
                    returnList.Add(chatroom);
            }
            return Json(returnList);
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
            bool adminChanged = false;
            Chatroom chatroom = await _context.Chatroom
                .Include(Chatroom => Chatroom.Members)
                .SingleAsync(Chatroom => Chatroom.ChatroomId == Convert.ToInt32(chatroomId));
            var memberIds = chatroom.Members.Select(m => m.Id).ToList();
            AppUser currentUser = await _userManager.GetUserAsync(User);

            if(chatroom.Members.Count == 1)
            {
                var returnObject = new { adminChanged = "", adminId = "" };
                DeleteChatroom(chatroom.ChatroomId);
                return Json(returnObject);
            }
            else
            {
                if (chatroom.AdminId == currentUser.Id)
                {
                    if (chatroom.Members.Count == 2)
                    {
                        string newAdminId = chatroom.Members
                            .Where(Member => Member.Id != currentUser.Id)
                            .Select(Member => Member.Id)
                            .Single();
                        chatroom.AdminId = newAdminId;
                    }
                    else
                    {
                        foreach (AppUser member in chatroom.Members)
                        {
                            if (member.Id != chatroom.AdminId)
                            {
                                chatroom.AdminId = member.Id;
                                break;
                            }
                        }
                    }
                    adminChanged = true;
                }
                var returnObject = new { adminChanged = adminChanged, adminId = chatroom.AdminId };
                chatroom.Members.Remove(currentUser);
                await _context.SaveChangesAsync();
                return Json(returnObject);
            }
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
