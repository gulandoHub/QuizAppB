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
    public class UserRightController : Controller
    {
        #region properties
        
        private readonly IUserRightService _userRightService;
        private readonly IMapper _mapper;
          
        #endregion

        #region ctor
        
        public UserRightController(IUserRightService userRightService, IMapper mapper)
        {
            _userRightService = userRightService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods

        [HttpGet("{userRightID}")]
        [Produces("application/json")]
        [ActionName("GetUserRightByID")]
        public async Task<JsonResult> GetUserRightByID(int userRightID)
        {
            var userRights = await _userRightService.GetUserRightSummaryAsync(userRightID);
            if (userRights != null && userRights.Count > 0)
            {
                var userRightData = _mapper.Map<UserRightData>(userRights.First());
                return Json(userRightData);                
            }
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllUserRights")]
        public async Task<JsonResult> GetAllUserRights()
        {
            var userRights = await _userRightService.GetUserRightSummaryAsync();
            if (userRights != null && userRights.Count > 0)
            {
                var userRightDataList = _mapper.Map<List<UserRightSummary>, List<UserRightData>>(userRights);
                return Json(userRightDataList);
            }
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("AddUserRight")]
        public async Task<JsonResult> AddUserRight([FromBody] UserRightData userRightData)
        {
            var userRight = _mapper.Map<UserRight>(userRightData);
            await _userRightService.AddUserRightAsync(userRight);
            return new JsonResult(null);
        }

        [HttpPut]
        [ActionName("UpdateUserRight")]
        public async Task<JsonResult> UpdateUserRight([FromBody] UserRightData userRightData)
        {
            var userRight = _mapper.Map<UserRight>(userRightData);
            await _userRightService.UpdateUserRightAsync(userRight);
            return new JsonResult(null);
        }
        
        [HttpDelete("{userRightID}")]
        [ActionName("DeleteUserRight")]
        public async Task<JsonResult> DeleteUserRight(int userRightID)
        {
            await _userRightService.DeleteUserRightAsync(userRightID);
            return new JsonResult(null);
        }
                
        #endregion
    }
}