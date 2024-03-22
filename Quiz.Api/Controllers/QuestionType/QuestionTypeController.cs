using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Models;
using QuizService;
using QuizData;


namespace QuizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class QuestionTypeController : Controller
    {
        
        #region properties
        
        private readonly IQuestionTypeService _questionTypeService;
        private readonly IMapper _mapper;
        
        #endregion

        #region ctor
        
        public QuestionTypeController(IQuestionTypeService service, IMapper mapper)
        {
            _questionTypeService = service;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods

        [HttpGet("{questionTypeID}")]
        [Produces("application/json")]
        [ActionName("GetQuestionTypeByID")]
        public async Task<JsonResult> GetQuestionTypeByID(int questionTypeID)
        {
            var questionType = await _questionTypeService.GetQuestionTypeSummaryAsync(questionTypeID);
            if (questionType != null && questionType.Count > 0)
            {
                var questionData = _mapper.Map<QuestionTypeData>(questionType.First());
                return Json(questionData);
            }
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuestionTypes")]
        public async Task<JsonResult> GetAllQuestionTypes()
        {
            var questionTypeList = await _questionTypeService.GetQuestionTypeSummaryAsync();
            if (questionTypeList != null && questionTypeList.Count > 0)
            {
                var questionData = _mapper.Map<List<QuestionTypeSummary>, List<QuestionTypeData>>(questionTypeList);
                return Json(questionData);
            }
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("AddQuestionType")]
        public async Task<JsonResult> AddQuestionType([FromBody] QuestionTypeData questionTypeData)
        {
            var questionType = _mapper.Map<QuestionType>(questionTypeData);
            await _questionTypeService.AddQuestionTypeAsync(questionType);
            return new JsonResult(null);
        }

        [HttpPut("{questionTypeID}")]
        [ActionName("UpdateQuestionType")]
        public async Task<JsonResult> UpdateQuestionType(int questionTypeID, [FromBody] QuestionTypeData questionTypeData)
        {
            var questionType = _mapper.Map<QuestionType>(questionTypeData);
            await _questionTypeService.UpdateQuestionTypeAsync(questionType);
            return new JsonResult(null);
        }
        
        [HttpDelete("{questionTypeID}")]
        [ActionName("DeleteQuestionType")]
        public async Task<JsonResult> DeleteQuestionType(int questionTypeID)
        {
            await _questionTypeService.DeleteQuestionTypeAsync(questionTypeID);
            return new JsonResult(null);
        }
        
        #endregion
        
    }
}