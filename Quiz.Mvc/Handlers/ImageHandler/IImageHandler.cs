using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace QuizMvc.Handlers.ImageHandler
{
    public interface IImageHandler
    {
        Task<IActionResult> UploadImage(IFormFile file, string ImageID);
    }
}