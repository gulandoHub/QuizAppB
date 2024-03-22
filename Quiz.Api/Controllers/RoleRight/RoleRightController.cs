using System.Collections.Generic;
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
    public class RoleRightController : Controller
    {
        #region properties
        

        private readonly IRoleRightService _roleRightService;
        private readonly IMapper _mapper;
          
        #endregion

        #region ctor
        
        public RoleRightController(IRoleRightService roleRightService, IMapper mapper)
        {
            _roleRightService = roleRightService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods

        [HttpGet("{roleRightID}")]
        [Produces("application/json")]
        [ActionName("GetRoleRightByID")]
        public async Task<JsonResult> GetRoleRightByID(int roleRightID)
        {
            var roleRights = await _roleRightService.GetRoleRightSummaryAsync(roleRightID);
            if (roleRights != null && roleRights.Count > 0)
            {
                var roleRightData = _mapper.Map<RoleRightData>(roleRights.First());
                return Json(roleRightData);                
            }
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllRoleRights")]
        public async Task<JsonResult> GetAllRoleRights()
        {
            var roleRights = await _roleRightService.GetRoleRightSummaryAsync();
            if (roleRights != null && roleRights.Count > 0)
            {
                var roleRightData = _mapper.Map<List<RoleRightSummary>, List<RoleRightData>>(roleRights);
                return Json(roleRightData);
            }
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("AddRoleRight")]
        public async Task<JsonResult> AddRoleRight([FromBody] RoleRightData roleRightData)
        {
            var roleRight = _mapper.Map<RoleRight>(roleRightData);
            await _roleRightService.AddRoleRightAsync(roleRight);
            return new JsonResult(null);
        }

        [HttpPut]
        [ActionName("UpdateRoleRight")]
        public async Task<JsonResult> UpdateRoleRight([FromBody] RoleRightData roleRightData)
        {
            var roleRight = _mapper.Map<RoleRight>(roleRightData);
            await _roleRightService.UpdateRoleRightAsync(roleRight);
            return new JsonResult(null);
        }
        
        [HttpDelete("{roleRightID}")]
        [ActionName("DeleteUserRole")]
        public async Task<JsonResult> DeleteUserRole(int roleRightID)
        {
            await _roleRightService.DeleteRoleRightAsync(roleRightID);
            return new JsonResult(null);
        }
                
        #endregion
    }
}