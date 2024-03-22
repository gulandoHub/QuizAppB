using Microsoft.AspNetCore.Mvc;


namespace QuizMvc.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index() => View("Error");
    }
}