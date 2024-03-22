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
    public class UserRightController : Controller
    {
        #region properties
        
        private readonly IUserRightService _userRightService;
        private readonly IUserService _userService;
        private readonly IRightService _rightService;
        private readonly IMapper _mapper;

        #endregion

        #region ctor
        
        public UserRightController(IUserRightService userRightService,IUserService userService,IRightService rightService, IMapper mapper)
        {
            _userRightService = userRightService;
            _userService = userService;
            _rightService = rightService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region  basic actions
        
        public IActionResult Index()
        {
            var userRights = _userRightService.GetUserRightSummary();
            var userRightDataList = _mapper.Map<IEnumerable<UserRightData>>(userRights);
            
            return View(userRightDataList);
        }
        
        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;

            var userRightSummaryList = _userRightService.GetUserRightSummary().First(userRight => userRight.ID == id);
            var userData = _mapper.Map<UserRightData>(userRightSummaryList);
            
            ViewData["Users"] = _userService.GetAllUsers().ToList();
            ViewData["Rights"] = _rightService.GetAllRights().ToList();
            
            return View("EditUserRight", userData);
        }
        
        [HttpPost]
        public IActionResult Edit(UserRightData userRightData)
        {
            if (!ModelState.IsValid)
                return null;
            
            var userRight = _mapper.Map<UserRight>(userRightData);
            _userRightService.UpdateUserRight(userRight);
            
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            var userRightData = new UserRightData();
                
            ViewData["Users"] = _userService.GetAllUsers().ToList();
            ViewData["Rights"] = _rightService.GetAllRights().ToList();
            
            return View("EditUserRight", userRightData );
        }
        
        [HttpPost]
        public IActionResult Create(UserRightData userRightData)
        {
            if (!ModelState.IsValid)
                return null;
            
            var userRight = _mapper.Map<UserRight>(userRightData);
            _userRightService.AddUserRight(userRight);
            
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userRightService.DeleteUserRight(id);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}