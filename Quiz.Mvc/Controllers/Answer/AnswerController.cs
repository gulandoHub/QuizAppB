using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizData;
using QuizMvc.Models;
using QuizService;


namespace QuizMvc.Controllers
{
    [Authorize]
    public class AnswerController : Controller
    {
        #region properties
        
        private readonly IAnswerService _answerService;
        private readonly IQuestionService _questionService;
        private readonly IAnswerTypeService _answerTypeService;
        private readonly IMapper _mapper;

        private List<Question> Questions => _questionService.GetAllQuestions().ToList(); 
        private List<AnswerType> AnswerTypes => _answerTypeService.GetAllAnswerTypes().ToList();
        
        #endregion

        #region ctor
        
        public AnswerController(IAnswerService service, IQuestionService questionService, IAnswerTypeService answerTypeService, IMapper mapper)
        {
            _answerService = service;
            _questionService = questionService;
            _answerTypeService = answerTypeService;
            _mapper = mapper;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var answerSummaryList = _answerService.GetAnswerSummary();
            var answerData = _mapper.Map<List<AnswerData>>(answerSummaryList);
            
            return View(answerData); 
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            ViewData["Questions"] = Questions;
            ViewData["AnswerTypes"] = AnswerTypes;
            
            var answerSummary = _answerService.GetAnswerSummary(id).First();
            var answerData = _mapper.Map<AnswerData>(answerSummary);
            
            return View("EditAnswer", answerData);
        }

        [HttpPost]
        public IActionResult Edit(AnswerData answerData)
        {
            if (!ModelState.IsValid)
                return null;
            
            var answer = _mapper.Map<Answer>(answerData);
            _answerService.UpdateAnswer(answer);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            ViewData["Questions"] = Questions;
            ViewData["AnswerTypes"] = AnswerTypes;
            
            return View("EditAnswer", new AnswerData());
        }
        
        [HttpPost]
        public IActionResult Create(AnswerData answerData)
        {
            var answer = _mapper.Map<Answer>(answerData);
            _answerService.AddAnswer(answer);
            
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _answerService.DeleteAnswer(id);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}