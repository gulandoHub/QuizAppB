using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;
using QuizMvc.Models;


namespace QuizMvc.Controllers
{
    [Authorize]
    public class QuizThemeController : Controller
    {
        #region properties
        
        private readonly IQuizThemeService _quizThemeService;
        private readonly IQuizService _quizService;
        private readonly IMapper _mapper;
        
        private List<QuizData.Quiz> Quizzes => _quizService.GetAllQuizes().ToList(); 
        
        #endregion

        #region ctor
        
        public QuizThemeController(IQuizThemeService service, IQuizService quizService, IMapper mapper)
        {
            _quizThemeService = service;
            _quizService = quizService;
            _mapper = mapper;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var quizThemes = _quizThemeService.GetQuizThemeSummary();
            return View(quizThemes);
        }
        
        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            ViewData["Quizes"] = Quizzes;

            var quizTheme = _quizThemeService.GetQuizThemeSummary(id).First();
            var quizThemeData = _mapper.Map<QuizThemeData>(quizTheme);
            
            return View("EditQuizTheme", quizThemeData);
        }
        
        [HttpPost]
        public IActionResult Edit(QuizTheme quizTheme)
        {
            if (!ModelState.IsValid)
                return null;
            
            _quizThemeService.UpdateQuizTheme(quizTheme);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            ViewData["Quizes"] = _quizService.GetAllQuizes().ToList();
            
            return View("EditQuizTheme", new QuizThemeData());
        }
        
        [HttpPost]
        public IActionResult Create(QuizTheme quizTheme)
        {
            if (!ModelState.IsValid)
                return null;
            
            _quizThemeService.AddQuizTheme(quizTheme);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _quizThemeService.DeleteQuizTheme(id);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}