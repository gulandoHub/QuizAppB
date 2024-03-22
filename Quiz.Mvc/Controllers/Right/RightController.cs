using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizData;
using QuizService;


namespace QuizApi.Controllers
{
    [Authorize]
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
        
        #region actions
        
        public IActionResult Index()
        {
            var rights = _rightService.GetAllRights();
            return View(rights);
        }
                
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _rightService.DeleteRight(id);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditRight", _rightService.GetRightByID(id));
        }
        
        [HttpPost]
        public IActionResult Edit(Right right)
        {
            _rightService.UpdateRight(right);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("EditRight", new Right());
        }
        
        [HttpPost]
        public IActionResult Create(Right right)
        {
            _rightService.AddRight(right);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
        
    }
}