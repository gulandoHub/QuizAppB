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
    public class RightController : Controller
    {
        
        #region properties
        
        private readonly IRightService _rightService;
        private readonly IMapper _mapper;
                
        #endregion

        #region ctor
        
        public RightController(IRightService rightService, IMapper mapper)
        {
            _rightService = rightService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods

        [HttpGet("{rightID}")]
        [Produces("application/json")]
        [ActionName("GetRightByID")]
        public async Task<JsonResult> GetRightByID(int rightID)
        {
            var right = await _rightService.GetRightByIDAsync(rightID);
            if (right != null)
                return Json(right);
            
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllRights")]
        public async Task<JsonResult> GetAllRights()
        {
            var rightList = await _rightService.GetAllRightsAsync();
            if (rightList != null && rightList.Count > 0)
                return Json(rightList);
            
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("AddRight")]
        public async Task<JsonResult> AddRight([FromBody] Right right)
        {
            await _rightService.AddRightAsync(right);
            return new JsonResult(null);
        }

        [HttpPut("{rightID}")]
        [ActionName("UpdateRight")]
        public async Task<JsonResult> UpdateRight(int rightID, [FromBody] Right right)
        {
            await _rightService.UpdateRightAsync(right);
            return new JsonResult(null);
        }
        
        [HttpDelete("{rightID}")]
        [ActionName("DeleteRight")]
        public async Task<JsonResult> DeleteRight(int rightID)
        {
            await _rightService.DeleteRightAsync(rightID);
            return new JsonResult(null);
        }
                
        #endregion
        
    }
}