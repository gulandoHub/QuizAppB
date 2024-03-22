using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;

namespace QuizService
{
    public interface IImageService
    {
        #region methods
        
        List<Image> GetAllImages();

        Image GetImageByID(int imageID);

        void UpdateImage(Image image);

        void AddImage(Image image);
        
        int InsertImageGetID(Image image);

        void DeleteImage(int imageID);

        #endregion
        
        #region async methods
        
        Task<List<Image>> GetAllImagesAsync();

        Task<Image> GetImageByIDAsync(int imageID);

        Task AddImageAsync(Image image);
        
        Task UpdateImageAsync(Image image);

        Task DeleteImageAsync(int imageID);

        #endregion  
    }
}