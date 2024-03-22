using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Helpers;
using QuizApi.Models;
using QuizData;
using QuizService;


namespace QuizApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
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
        
        #region api methods

        [HttpGet("{userID}")]
        [Produces("application/json")]
        [ActionName("GetUserByID")]
        public async Task<JsonResult> GetUserByID(int userID)
        {
            var user = await _userService.GetUserByIDAsync(userID);
            if (user != null )
            {
                var userData = _mapper.Map<UserData>(user);
                return Json(userData);                
            }
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllUsers")]
        public async Task<JsonResult> GetAllUsers()
        {
            var userList = await _userService.GetAllUsersAsync();
            if (userList != null && userList.Count > 0)
            {
                var userListData = _mapper.Map<List<User>, List<UserData>>(userList);
                return Json(userListData);
            }
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("RegisterUser")]
        public async Task<JsonResult> AddUser([FromBody] UserData userData)
        {
            var user = _mapper.Map<User>(userData);
            await _userService.Create(user,userData.Password);
            return new JsonResult(null);
        }

        [HttpPut]
        [ActionName("UpdateUser")]
        public async Task<JsonResult> UpdateUser([FromBody] UserData userData)
        {
            var user = _mapper.Map<User>(userData);
            await _userService.Update(user, userData.Password);
            return new JsonResult(null);
        }
        
        [HttpDelete("{userID}")]
        [ActionName("DeleteUser")]
        public async Task<JsonResult> DeleteUser(int userID)
        {
            await _userService.DeleteUserAsync(userID);
            return new JsonResult(null);
        }
        
        [AllowAnonymous]
        public async Task<JsonResult> Authenticate([FromBody]UserData userDto)
        {
            var user = await _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return new JsonResult(new {message = "Username or password is incorrect"});

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new JsonResult(new {
                Id = user.ID,
                user.Username,
                user.FirstName,
                user.LastName,
                Token = tokenString
            });
        }
        
        #endregion
    }
}