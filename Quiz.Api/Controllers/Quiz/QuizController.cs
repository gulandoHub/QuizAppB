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
    public class QuizController : Controller
    {
        
        #region properties
        
        private readonly IQuizService _quizService;
        private readonly IMapper _mapper;
        
        #endregion

        #region ctor
        
        public QuizController(IQuizService service, IMapper mapper)
        {
            _quizService = service;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods

        [HttpGet("{quizID}")]
        [Produces("application/json")]
        [ActionName("GetQuizByID")]
        public async Task<JsonResult> GetQuizByID(int quizID)
        {
            var quiz = await _quizService.GetQuizByIDAsync(quizID);
            if (quiz != null)
                return Json(quiz);
            
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuizes")]
        public async Task<JsonResult> GetAllQuizes()
        {
            var quizList = await _quizService.GetAllQuizesAsync();
            if (quizList != null && quizList.Count > 0)
                return Json(quizList);
            
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("AddQuiz")]
        public async Task<JsonResult> AddQuiz([FromBody] Quiz quiz)
        {
            await _quizService.AddQuizAsync(quiz);
            return new JsonResult(null);
        }

        [HttpPut("{quizID}")]
        [ActionName("UpdateQuiz")]
        public async Task<JsonResult> UpdateQuiz([FromBody] Quiz quiz)
        {
            await _quizService.UpdateQuizAsync(quiz);
            return new JsonResult(null);
        }
        
        [HttpDelete("{quizID}")]
        [ActionName("DeleteQuiz")]
        public async Task<JsonResult> DeleteQuiz(int quizID)
        {
            await _quizService.DeleteQuizAsync(quizID);
            return new JsonResult(null);
        }

        #endregion

        #region other

        [Produces("application/json")]
        [ActionName("GetAllQuizzesWithChild")]
        public async Task<JsonResult> GetAllQuizzesWithChild()
        {
            var quizList = await _quizService.GetAllQuizzesWithChild();
            if (quizList != null && quizList.Count > 0)
                return Json(quizList);

            return new JsonResult(null);
        }

        [HttpGet("{quizID}")]
        [Produces("application/json")]
        [ActionName("GetAllQuizThemesByQuizID")]
        public async Task<JsonResult> GetAllQuizThemesByQuizID(int quizID)
        {
            var quizThemes = await _quizService.GetAllQuizThemesByQuizIDAsync(quizID);
            if (quizThemes != null && quizThemes.Count > 0)
            {
                var quizThemeDataList = _mapper.Map<List<QuizThemeSummary>, List<QuizThemeData>>(quizThemes);
                return Json(quizThemeDataList);
            }

            return new JsonResult(null);
        }

        [HttpGet("{quizID}")]
        [Produces("application/json")]
        [ActionName("GetAllQuestionsByQuizThemes")]
        public async Task<JsonResult> GetAllQuestionsByQuizThemes(int quizID, [FromQuery]List<int> quizThemeIDs)
        {
            var questions = await _quizService.GetAllQuestionsByQuizThemesAsync(quizID, quizThemeIDs);

            if (questions != null && questions.Count > 0)
            {
                var questionDataList = _mapper.Map<List<QuestionSummary>, List<QuestionData>>(questions);
                return Json(questionDataList);
            }

            return new JsonResult(null);
        }

        [HttpGet("{quizID}/{examTypeID}")]
        [Produces("application/json")]
        [ActionName("GetAllQuestionsByExamType")]
        public async Task<JsonResult> GetAllQuestionsByExamType(int quizID, int examTypeID)
        {
            var questions = await _quizService.GetAllQuestionsByExamTypeAsync(quizID, examTypeID);

            if (questions != null && questions.Count > 0)
            {
                var questionDataList = _mapper.Map<List<QuestionSummary>, List<QuestionData>>(questions);
                return Json(questionDataList.Take(50));
            }

            return new JsonResult(null);
        }

        #endregion
    }
}