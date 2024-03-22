using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizUtils.ImageWriter;


namespace QuizMvc.Handlers.ImageHandler
{
    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        
        public ImageHandler(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public async Task<IActionResult> UploadImage(IFormFile file, string imageID)
        {
            var result = await _imageWriter.UploadImage(file, imageID);
            return new ObjectResult(result);
        }
    }
}