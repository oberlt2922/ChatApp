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
        public async Task<IActionResult> CreateChatroom(string isPublic, string chatroomName, string[] usernames)
        {
            try
            {
                if (string.IsNullOrEmpty(isPublic) || string.IsNullOrEmpty(chatroomName))
                {
                    return BadRequest();
                }

                AppUser currentUser = await _userManager.GetUserAsync(User);
                if(currentUser == null)
                {
                    throw new Exception();
                }

                Chatroom room = new Chatroom(chatroomName, currentUser.Id, isPublic == "Public" ? true : false, new List<AppUser>(), new List<Message>());
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
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Adds members to a chatroom
        /// </summary>
        /// <param name="chatroomId">The id of the chatroom that the members are being added to</param>
        /// <param name="members">The usernames of the members being added to the chatroom</param>
        /// <returns>Returns the chatroom id and the new members' ids as json object</returns>
        [HttpPost]
        public async Task<IActionResult> AddMembers(string chatroomId, string[] usernames)
        {
            try
            {
                if (string.IsNullOrEmpty(chatroomId) || usernames.Length == 0)
                {
                    return BadRequest();
                }

                Chatroom room = _context.Chatroom.Where(c => c.ChatroomId == Convert.ToInt32(chatroomId))
                    .Include(c => c.Members).Single();

                if (room == null)
                {
                    return NotFound();
                }

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
                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //Fetches and returns the chatroom as Json
        [HttpGet]
        public IActionResult GetChatroom(string chatroomId)
        {
            try
            {
                if (string.IsNullOrEmpty(chatroomId))
                {
                    return BadRequest();
                }

                Chatroom chatroom = _context.Chatroom
                    .Include(Chatroom => Chatroom.Messages.OrderBy(Message => Message.Sent))
                    .Include(Chatroom => Chatroom.Members)
                    .Single(Chatroom => Chatroom.ChatroomId == Convert.ToInt32(chatroomId));

                if (chatroom == null)
                {
                    return NotFound();
                }

                return Ok(chatroom);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //get chatrooms by prefix where chatroom is public and user is not already a member
        [HttpGet]
        public async Task<IActionResult> AutoCompleteChatroom(string prefix, string userId)
        {
            try
            {
                if(string.IsNullOrEmpty(userId))
                {
                    return BadRequest();
                }

                List<Chatroom> chatrooms = await _context.Chatroom
                        .Include(Chatroom => Chatroom.Members)
                        .Where(Chatroom => Chatroom.ChatroomName.StartsWith(prefix)
                            && Chatroom.IsPublic == true
                            && !(Chatroom.Members.Any(m => m.Id == userId) || (_context.BlockedUsers.Any(b => b.UserId == userId && b.ChatroomId == Chatroom.ChatroomId))))
                        .ToListAsync();
                return Json(chatrooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //join chatroom accept chatroom id and user id return chatroom json result
        [HttpPost]
        public async Task<IActionResult> JoinChatroom(string chatroomId)
        {
            try
            {
                if (string.IsNullOrEmpty(chatroomId))
                {
                    return BadRequest();
                }

                Chatroom chatroom = _context.Chatroom
                    .Include(Chatroom => Chatroom.Messages.OrderBy(Message => Message.Sent))
                    .Include(Chatroom => Chatroom.Members)
                    .Single(Chatroom => Chatroom.ChatroomId == Convert.ToInt32(chatroomId));

                if (chatroom == null)
                {
                    return NotFound();
                }

                AppUser currentUser = await _userManager.GetUserAsync(User);

                if(currentUser == null)
                {
                    throw new Exception();
                }

                chatroom.Members.Add(currentUser);
                _context.SaveChanges();
                return Json(chatroom);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //leave chatroom and choose new admin if current user is admin
        [HttpPost]
        public async Task<IActionResult> LeaveChatroom(string chatroomId)
        {
            try
            {
                if (string.IsNullOrEmpty(chatroomId))
                {
                    return BadRequest();
                }

                Chatroom chatroom = await _context.Chatroom
                    .Include(Chatroom => Chatroom.Members).
                        ThenInclude(m => m.Messages).
                    SingleAsync(Chatroom => Chatroom.ChatroomId == Convert.ToInt32(chatroomId));

                if (chatroom == null)
                {
                    return NotFound();
                }

                AppUser currentUser = await _userManager.GetUserAsync(User);
                bool adminChanged = false;
                string adminUsername = "";

                if (chatroom.Members.Count == 1)
                {
                    DeleteChatroom(chatroom.ChatroomId.ToString());
                    return Ok(new { deleted = true });
                }
                if (chatroom.AdminId == currentUser.Id)
                {
                    adminChanged = true;
                    var newAdmin = chatroom.Members.
                        Where(m => m.Id != chatroom.AdminId).
                        Select(m => new { UserId = m.Id, count = m.Messages.Where(msg => msg.ChatroomId == chatroom.ChatroomId).Count() }).
                        OrderByDescending(g => g.count).
                        First();
                    chatroom.AdminId = newAdmin.UserId;
                    adminUsername = _context.AppUser.Where(a => a.Id == newAdmin.UserId).Select(u => u.UserName).Single();

                    if (newAdmin == null || adminUsername == null)
                    {
                        throw new Exception();
                    }
                }

                var returnObject = new { adminChanged = adminChanged, adminId = chatroom.AdminId, adminUsername = adminUsername };
                chatroom.Members.Remove(currentUser);
                await _context.SaveChangesAsync();
                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //delete chatroom
        [HttpPost]
        public IActionResult DeleteChatroom(string chatroomId)
        {
            try
            {
                if(string.IsNullOrEmpty(chatroomId))
                {
                    return BadRequest();
                }
                Chatroom chatroom = _context.Chatroom.Include(c => c.Messages).Single(c => c.ChatroomId == Convert.ToInt32(chatroomId));
                if (chatroom == null) return NotFound();
                _context.Remove(chatroom);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        //check if user exists by username
        [HttpGet]
        public async Task<IActionResult> UserExists(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest();
            }

            try
            {
                return Ok(new { Exists = await _context.AppUser.AnyAsync(a => a.UserName == username) });
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
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
