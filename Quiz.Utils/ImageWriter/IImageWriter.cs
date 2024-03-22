using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace QuizUtils.ImageWriter
{
    public interface IImageWriter
    {
        Task<string> UploadImage(IFormFile file, string imageID);
    }
}