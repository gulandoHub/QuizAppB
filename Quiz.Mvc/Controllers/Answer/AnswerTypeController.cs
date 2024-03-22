using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizData;
using QuizMvc.Helpers;
using QuizMvc.Models;
using QuizService;


namespace QuizMvc.Controllers
{
    [Authorize]
    public class AnswerTypeController : Controller
    {
        #region properties
        
        private readonly IAnswerTypeService _answerTypeService;
        private readonly IQuizService _quizService;
        private readonly IQuestionTypeService _questionTypeService;
        private readonly IMapper _mapper;
        
        private List<QuizData.Quiz> Quizzes => _quizService.GetAllQuizes().ToList(); 
        private List<QuestionType> QuestionTypes => _questionTypeService.GetAllQuestionTypes().ToList();
        
        #endregion

        #region ctor
        
        public AnswerTypeController(IAnswerTypeService service, IQuizService quizService, IQuestionTypeService questionTypeService, IMapper mapper)
        {
            _answerTypeService = service;
            _quizService = quizService;
            _questionTypeService = questionTypeService;
            _mapper = mapper;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var answerTypes = _answerTypeService.GetAnswerTypeSummary();
            return View(answerTypes);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
                        
            var answerTypeSummary = _answerTypeService.GetAnswerTypeSummary(id).First();
            var answerTypeData = _mapper.Map<AnswerTypeData>(answerTypeSummary);

            ViewData["Quizes"] = Quizzes;
            ViewData["QuestionTypes"] = QuestionTypes.Where(questionType => questionType.QuizID == answerTypeData.QuizID);
            
            return View("EditAnswerType", answerTypeData);
        }

        [HttpPost]
        public IActionResult Edit(AnswerType answerType)
        {
            if (!ModelState.IsValid)
                return null;
            
            
            _answerTypeService.UpdateAnswerType(answerType);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;

            ViewData["Quizes"] = Quizzes;
            ViewData["QuestionTypes"] = QuestionTypes;
            
            return View("EditAnswerType", new AnswerTypeData());
        }
        
        [HttpPost]
        public IActionResult Create(AnswerType answerType)
        {
            if (!ModelState.IsValid)
                return null;

            answerType.AnswerTypeDescription = "<AnswerType></AnswerType>";
            _answerTypeService.AddAnswerType(answerType);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _answerTypeService.DeleteAnswerType(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}