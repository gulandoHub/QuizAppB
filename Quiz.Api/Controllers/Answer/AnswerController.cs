using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using QuizApi.Models;
using QuizService;
using QuizData;


namespace QuizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AnswerController : Controller
    {
        
        #region properties
        
        private readonly IAnswerService _answerService;
        private readonly IMapper _mapper;

        #endregion

        #region ctor
        
        public AnswerController(IMapper mapper, IServiceProvider serviceProvider)
        {
            _answerService = ActivatorUtilities.CreateInstance<AnswerService>(serviceProvider);
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods

        [HttpGet("{answerID}")]
        [Produces("application/json")]
        [ActionName("GetAnswerByID")]
        public async Task<JsonResult> GetAnswer(int answerID)
        {
            var answer = await _answerService.GetAnswerSummaryAsync(answerID);
            if (answer != null && answer.Count > 0)
            {
                var answerData = _mapper.Map<AnswerData>(answer.First());
                return Json(answerData);                
            }
            
            return new JsonResult(null);

        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllAnswers")]
        public async Task<JsonResult> GetAllAnswers()
        {
            var answers = await _answerService.GetAnswerSummaryAsync();

            if (answers != null && answers.Count > 0)
            {
                var answerDataList = _mapper.Map<List<AnswerSummary>, List<AnswerData>>(answers);
                return Json(answerDataList);                
            }
            
            return new JsonResult(null);

        }

        [HttpPost]
        [ActionName("AddAnswer")]
        public async Task<JsonResult> AddAnswer([FromBody] AnswerData answerData)
        {
            var answer = _mapper.Map<Answer>(answerData);
            await _answerService.AddAnswerAsync(answer);
            return new JsonResult(null);
        }

        [HttpPut]
        [ActionName("UpdateAnswer")]
        public async Task<JsonResult> UpdateAnswer([FromBody] AnswerData answerData)
        {
            var answer = _mapper.Map<Answer>(answerData);
            await _answerService.UpdateAnswerAsync(answer);
            return new JsonResult(null);
        }
        
        [HttpDelete("{answerID}")]
        [ActionName("DeleteAnswer")]
        public async Task<JsonResult> DeleteAnswer(int answerID)
        {
            await _answerService.DeleteAnswerAsync(answerID);
            return new JsonResult(null);
        }
        
        #endregion
        
    }
}