using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizData;
using QuizService;


namespace QuizApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class RoleController : Controller
    {
        
        #region properties
        
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
                
        #endregion

        #region ctor
        
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods

        [HttpGet("{roleID}")]
        [Produces("application/json")]
        [ActionName("GetRoleByID")]
        public async Task<JsonResult> GetRoleByID(int roleID)
        {
            var role = await _roleService.GetRoleByIDAsync(roleID);
            if (role != null)
                return Json(role);
            
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllRoles")]
        public async Task<JsonResult> GetAllRoles()
        {
            var roleList = await _roleService.GetAllRolesAsync();
            if (roleList != null && roleList.Count > 0)
                return Json(roleList);
            
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("AddRole")]
        public async Task<JsonResult> AddRole([FromBody] Role role)
        {
            await _roleService.AddRoleAsync(role);
            return new JsonResult(null);
        }

        [HttpPut("{roleID}")]
        [ActionName("UpdateRole")]
        public async Task<JsonResult> UpdateRole(int roleID, [FromBody] Role role)
        {
            await _roleService.UpdateRoleAsync(role);
            return new JsonResult(null);
        }
        
        [HttpDelete("{roleID}")]
        [ActionName("DeleteRole")]
        public async Task<JsonResult> DeleteRole(int roleID)
        {
            await _roleService.DeleteRoleAsync(roleID);
            return new JsonResult(null);
        }
                
        #endregion
        
    }
}