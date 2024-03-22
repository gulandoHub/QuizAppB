using Microsoft.AspNetCore.Mvc;


namespace QuizMvc.Controllers
{
    public class Home1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}