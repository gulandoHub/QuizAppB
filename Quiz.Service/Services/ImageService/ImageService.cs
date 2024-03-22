using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class ImageService : IImageService
    {
        #region properties
        
        private readonly IRepository<Image> _imageRepository;

        #endregion
        
        #region ctor

        public ImageService(IRepository<Image> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        #endregion
        
        #region methods
        
        public List<Image> GetAllImages()
        {
            return _imageRepository.Table.ToList();
        }

        public Image GetImageByID(int imageID)
        {
            return _imageRepository.GetById(imageID);
        }

        public void UpdateImage(Image image)
        {
            _imageRepository.Update(image);
        }

        public void AddImage(Image image)
        {
            _imageRepository.Insert(image);
        }

        public int InsertImageGetID(Image image)
        {
           return _imageRepository.InsertGetID(image);
        }

        public void DeleteImage(int imageID)
        {
            _imageRepository.Delete(imageID);
        }

        #endregion
        
        #region async methods
        
        public async Task<List<Image>> GetAllImagesAsync()
        {
            return await _imageRepository.Table.ToListAsync();
        }

        public async Task<Image> GetImageByIDAsync(int imageID)
        {
            return await _imageRepository.GetByIdAsync(imageID);
        }

        public async Task AddImageAsync(Image image)
        {
            await _imageRepository.InsertAsync(image);
        }

        public async Task UpdateImageAsync(Image image)
        {
            await _imageRepository.UpdateAsync(image);
        }

        public async Task DeleteImageAsync(int imageID)
        {
            await _imageRepository.DeleteAsync(imageID);
        }
        
        #endregion
    }
}