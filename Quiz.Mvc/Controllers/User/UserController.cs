using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QuizData;
using QuizMvc.Helpers;
using QuizMvc.Models;
using QuizService;


namespace QuizMvc.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        #region properties
        
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;


        
        #endregion

        #region ctor
        
        public UserController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        
        #endregion
        
        #region basic actions

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            var userDataList = _mapper.Map<IEnumerable<UserData>>(users);
            return View(userDataList);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.CreateMode = false;
            var user = await _userService.GetUserByIDAsync(id);
            var userData = _mapper.Map<UserData>(user);
            return View("EditUser", userData);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(UserData userData)
        {
            if (!ModelState.IsValid)
                return null;
                
            var user = _mapper.Map<User>(userData);
            await _userService.Update(user, userData.Password);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("EditUser", new UserData());
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(UserData userData)
        {
            if (!ModelState.IsValid)
                return null;
            
            var user = _mapper.Map<User>(userData);
            await _userService.Create(user,userData.Password);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
        
        #region login/register
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginData userData)
        {
            if (!ModelState.IsValid) return View(userData);
            
            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Username == userData.Username && u.Password == userData.Password);
            if (user != null)
            {
                await Authenticate(userData.Username);
                return RedirectToAction("Index", "Home");
            }
                
            ModelState.AddModelError("", "Incorrect Login or Email");
            return View(userData);
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserData userData)
        {
            if (!ModelState.IsValid) return View(userData);
            
            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Username == userData.Username);
            if (user == null)
            {
                var newUser = _mapper.Map<User>(userData);
                await _userService.Create(newUser,userData.Password);
 
                await Authenticate(userData.Username);
 
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Incorrect Login or UserName");
            return View(userData);
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            
            
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        
        #endregion

    }
}