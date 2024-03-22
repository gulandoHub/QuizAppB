using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizData;
using QuizService;


namespace QuizApi.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        
        #region properties
        
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
                
        #endregion

        #region ctor
        
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region actions
        
        public IActionResult Index()
        {
            var roles = _roleService.GetAllRoles();
            return View(roles);
        }
                
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _roleService.DeleteRole(id);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditRole", _roleService.GetRoleByID(id));
        }
        
        [HttpPost]
        public IActionResult Edit(Role role)
        {
            _roleService.UpdateRole(role);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("EditRole", new Role());
        }
        
        [HttpPost]
        public IActionResult Create(Role role)
        {
            _roleService.AddRole(role);
            return RedirectToAction(nameof(Index));
        }
                
        #endregion
        
    }
}