using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Models;
using QuizService;
using QuizData;
using QuizApi.Helpers;

namespace QuizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class QuestionController : Controller
    {
        
        #region properties
        
        private readonly IQuestionService _questionService;
        private readonly IAnswerTypeService _answerTypeService;
        private readonly IMapper _mapper;
        
        #endregion

        #region ctor
        
        public QuestionController(IQuestionService questionService, IAnswerTypeService answerTypeService, IMapper mapper)
        {
            _questionService = questionService;
            _answerTypeService = answerTypeService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods

        [HttpGet("{questionID}")]
        [Produces("application/json")]
        [ActionName("GetQuestionByID")]
        public async Task<JsonResult> GetQuestionByID(int questionID)
        {
            var question = await _questionService.GetQuestionSummaryAsync(questionID);
            if (question != null && question.Count > 0)
            {
                var questionData = _mapper.Map<QuestionData>(question.First());
                return Json(questionData);                
            }
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuestions")]
        public async Task<JsonResult> GetAllQuestions()
        {
            var questions = await _questionService.GetQuestionSummaryAsync();
            if (questions != null && questions.Count > 0)
            {
                var questionData = _mapper.Map<List<QuestionSummary>, List<QuestionData>>(questions);
                return Json(questionData);
            }
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("AddQuestion")]
        public async Task<JsonResult> AddQuestion([FromBody] QuestionData questionData)
        {
            var question = _mapper.Map<Question>(questionData);
            await _questionService.AddQuestionAsync(question);
            return new JsonResult(null);
        }

        [HttpPut("{questionID}")]
        [ActionName("UpdateQuestion")]
        public async Task<JsonResult> UpdateQuestion(int questionID, [FromBody] QuestionData questionData)
        {
            var question = _mapper.Map<Question>(questionData);
            await _questionService.UpdateQuestionAsync(question);
            return new JsonResult(null);
        }
        
        [HttpDelete("{questionID}")]
        [ActionName("DeleteQuestion")]
        public async Task<JsonResult> DeleteQuestion(int questionID)
        {
            await _questionService.DeleteQuestionAsync(questionID);
            return new JsonResult(null);
        }

        #endregion

        #region other

        // 1  130
        // 2  1,2,3
        // 3  '1:1,2,3', '2:3,9', '3"7,8,9'

        [HttpGet("{questionID}")]
        [Produces("application/json")]
        [ActionName("IsAnswerCorrect")]
        public async Task<JsonResult> IsAnswerCorrect(int questionID, [FromQuery]List<string> answers)
        {
            var question = _questionService.GetQuestionByID(questionID);

            var answerType = await _answerTypeService.GetAnswerTypeByIDAsync(question.AnswerTypeID);
            var answerTypeConfiguration = GetAnswerTypeConfiguration(question.AnswerTypeID);

            var resultData = new ResultData();
            int score = 0;

            if (answers.Count == 1)
            {
                if (question.CorrectAnswer.Equals(answers[0]))
                    score = 1;
                else
                {

                }
            }
            else
            {
                
            }

            resultData.CorrectAnswer = question.CorrectAnswer;
            resultData.Score = score;
            resultData.MaxScore = answerTypeConfiguration.CorrectCount;


            return new JsonResult(resultData);
        }

        private AnswerTypeConfiguration GetAnswerTypeConfiguration(int answerTypeID)
        {
            if (answerTypeID > 0)
            {
                var answerType = _answerTypeService.GetAnswerTypeByID(answerTypeID);
                var answerTypeDescriptionElement = answerType.AnswerTypeDescription;
                var answerTypeConfiguration = Util.Deserialize<AnswerTypeConfiguration>(answerTypeDescriptionElement);

                return answerTypeConfiguration;
            }
            return null;
        }

        #endregion

    }
} 