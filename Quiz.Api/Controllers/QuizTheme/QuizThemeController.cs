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
    public class QuizThemeController : Controller
    {
        
        #region properties
        
        private readonly IQuizThemeService _quizThemeService;
        private readonly IMapper _mapper;
        
        #endregion

        #region ctor
        
        public QuizThemeController(IQuizThemeService service, IMapper mapper)
        {
            _quizThemeService = service;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods

        [HttpGet("{quizThemeID}")]
        [Produces("application/json")]
        [ActionName("GetQuizThemeByID")]
        public async Task<JsonResult> GetQuizTheme(int quizThemeID)
        {
            var quizTheme = await _quizThemeService.GetQuizThemeSummaryAsync(quizThemeID);
            if (quizTheme != null && quizTheme.Count > 0)
            {
                var quizThemeData = _mapper.Map<QuizTheme>(quizTheme.First());
                return Json(quizThemeData);                
            }
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuizThemes")]
        public async Task<JsonResult> GetAllQuizThemes()
        {
            var quizThemeList = await _quizThemeService.GetQuizThemeSummaryAsync();
            if (quizThemeList != null && quizThemeList.Count > 0)
            {
                var quizThemeData = _mapper.Map<List<QuizThemeSummary>, List<QuizThemeData>>(quizThemeList);
                return Json(quizThemeData);
            }
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("AddQuizTheme")]
        public async Task<JsonResult> AddQuizTheme([FromBody] QuizThemeData quizThemeData)
        {
            var quizTheme = _mapper.Map<QuizTheme>(quizThemeData);
            await _quizThemeService.AddQuizThemeAsync(quizTheme);
            return new JsonResult(null);
        }

        [HttpPut("{quizThemeID}")]
        [ActionName("UpdateQuizTheme")]
        public async Task<JsonResult> UpdateQuizTheme(int quizThemeID, [FromBody] QuizThemeData quizThemeData)
        {
            var quizTheme = _mapper.Map<QuizTheme>(quizThemeData);
            await _quizThemeService.UpdateQuizThemeAsync(quizTheme);
            return new JsonResult(null);
        }
        
        [HttpDelete("{quizThemeID}")]
        [ActionName("DeleteQuizTheme")]
        public async Task<JsonResult> DeleteQuizTheme(int quizThemeID)
        {
            await _quizThemeService.DeleteQuizThemeAsync(quizThemeID);
            return new JsonResult(null);
        }
        
        #endregion
        
    }
}