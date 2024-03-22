using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Models;
using QuizData;
using QuizService;


namespace QuizApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class UserRoleController : Controller
    {
        #region properties
        
        private readonly IUserRoleService _userRoleService;
        private readonly IMapper _mapper;
          
        #endregion

        #region ctor
        
        public UserRoleController(IUserService userService, IUserRoleService userRoleService, IRoleService roleService, IMapper mapper)
        {
            _userRoleService = userRoleService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods

        [HttpGet("{userRoleID}")]
        [Produces("application/json")]
        [ActionName("GetUserRoleByID")]
        public async Task<JsonResult> GetUserRoleByID(int userRoleID)
        {
            var userRoles = await _userRoleService.GetUserRoleSummaryAsync(userRoleID);
            if (userRoles != null && userRoles.Count > 0)
            {
                var userRoleData = _mapper.Map<UserRoleData>(userRoles.First());
                return Json(userRoleData);            
            }
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllUserRoles")]
        public async Task<JsonResult> GetAllUserRoles()
        {
            var userRoles = await _userRoleService.GetUserRoleSummaryAsync();
            if (userRoles != null && userRoles.Count > 0)
            {
                var userRoleData = _mapper.Map<UserRoleData>(userRoles.First());
                return Json(userRoleData);            
            }
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("AddUserRole")]
        public async Task<JsonResult> AddUserRole([FromBody] UserRoleData userRoleData)
        {
            var userRole = _mapper.Map<UserRole>(userRoleData);
            await _userRoleService.AddUserRoleAsync(userRole);
            return new JsonResult(null);
        }

        [HttpPut]
        [ActionName("UpdateUserRole")]
        public async Task<JsonResult> UpdateUserRole([FromBody] UserRoleData userRoleData)
        {
            var userRole = _mapper.Map<UserRole>(userRoleData);
            await _userRoleService.UpdateUserRoleAsync(userRole);
            return new JsonResult(null);
        }
        
        [HttpDelete("{userRoleID}")]
        [ActionName("DeleteUserRole")]
        public async Task<JsonResult> DeleteUserRole(int userRoleID)
        {
            await _userRoleService.DeleteUserRoleAsync(userRoleID);
            return new JsonResult(null);
        }
                
        #endregion
    }
}