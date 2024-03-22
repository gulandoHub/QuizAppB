using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;
using QuizMvc.Helpers;


namespace QuizMvc.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        
        #region properties
        
        private readonly IQuizService _quizService;
        private readonly IAnswerTypeService _answerTypeService;
        private readonly IMapper _mapper;
        
        #endregion

        #region ctor
        
        public QuizController(IQuizService service, IMapper mapper, IAnswerTypeService answerTypeService)
        {
            _quizService = service;
            _answerTypeService = answerTypeService;
            _mapper = mapper;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var quizes = _quizService.GetAllQuizes();
            return View(quizes);
        }

        [HttpGet]
        public ActionResult GetQuizSummary(int quizID, int questionTypeID)
        {
            var quizSummary = _quizService.GetQuizSummary(quizID, questionTypeID);
            var quizData = _mapper.Map<List<QuizSummary>, List<Models.QuizData>>(quizSummary);

            return Json(quizData);
        }
        
        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditQuiz", _quizService.GetQuizByID(id));
        }

        [HttpPost]
        public IActionResult Edit(QuizData.Quiz quiz)
        {
            if (!ModelState.IsValid)
                return null;
            
            _quizService.UpdateQuiz(quiz);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("EditQuiz", new QuizData.Quiz());
        }
        
        [HttpPost]
        public IActionResult Create(QuizData.Quiz quiz)
        {
            if (!ModelState.IsValid)
                return null;
            
            _quizService.AddQuiz(quiz);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _quizService.DeleteQuiz(id);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}