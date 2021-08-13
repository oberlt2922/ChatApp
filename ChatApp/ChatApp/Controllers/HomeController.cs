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
                            .ThenInclude(Message => Message.Sender)
                        .Include(AppUser => AppUser.Chatrooms)
                            .ThenInclude(Chatroom => Chatroom.Members)
                    .SingleAsync(AppUser => AppUser.Id == currentUser.Id);
                return View(currentUser);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateChatroom(string isPublic, string chatroomName, string[] username)
        {
            Chatroom room = new Chatroom();
            AppUser currentUser = await _userManager.GetUserAsync(User);
            room.AdminId = currentUser.Id;
            room.ChatroomName = chatroomName;
            room.IsPublic = (isPublic == "Public") ? true : false;
            room.Members = new List<AppUser>();
            room.Messages = new List<Message>();
            room.Members.Add(currentUser);
            foreach(string user in username)
            {
                AppUser member = await _context.AppUser.SingleOrDefaultAsync(a => a.UserName == user);
                if(member != null)
                {
                    room.Members.Add(member);
                }
            }
            _context.Chatroom.Add(room);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
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
