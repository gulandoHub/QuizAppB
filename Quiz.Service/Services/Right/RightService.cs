using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class RightService : IRightService
    {
        #region properties

        private readonly IRepository<Right> _rightRepository;
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor
        
        public RightService(IRepository<Right> rightRepository,IMemoryCache memoryCache)
        {
            _rightRepository = rightRepository;
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods
        
        public List<Right> GetAllRights()
        {
            if (_memoryCache.TryGetValue(RightDefaults.RightAllCacheKey, out List<Right> rights)) 
                return rights.ToList();
                
            rights = _rightRepository.Table.ToList();
            _memoryCache.Set(RightDefaults.RightAllCacheKey, rights);

            return rights.ToList();
        }

        public Right GetRightByID(int rightID)
        {
            return _rightRepository.GetById(rightID);
        }

        public void UpdateRight(Right right)
        {
            _memoryCache.Remove(RightDefaults.RightAllCacheKey);
            _memoryCache.Remove(RightDefaults.RightByIdCacheKey);
            
            _rightRepository.Update(right);
        }

        public void AddRight(Right right)
        {
            _memoryCache.Remove(RightDefaults.RightAllCacheKey);
            _memoryCache.Remove(RightDefaults.RightByIdCacheKey);
            
            _rightRepository.Insert(right);
        }

        public void DeleteRight(int rightID)
        {
            _memoryCache.Remove(RightDefaults.RightAllCacheKey);
            _memoryCache.Remove(RightDefaults.RightByIdCacheKey);
            
            _rightRepository.Delete(rightID);
        }

        #endregion
        
        #region async methods
        
        public async Task<List<Right>> GetAllRightsAsync()
        {
            if (_memoryCache.TryGetValue(RightDefaults.RightAllCacheKey, out List<Right> rights)) 
                return rights.ToList();
                
            rights = await _rightRepository.Table.ToListAsync();
            _memoryCache.Set(RightDefaults.RightAllCacheKey, rights);

            return rights.ToList();
        }

        public async Task<Right> GetRightByIDAsync(int rightID)
        {
            return await _rightRepository.GetByIdAsync(rightID);
        }

        public async Task AddRightAsync(Right right)
        {
            _memoryCache.Remove(RightDefaults.RightAllCacheKey);
            _memoryCache.Remove(RightDefaults.RightByIdCacheKey);
            
            await _rightRepository.InsertAsync(right);
        }

        public async Task UpdateRightAsync(Right right)
        {
            _memoryCache.Remove(RightDefaults.RightAllCacheKey);
            _memoryCache.Remove(RightDefaults.RightByIdCacheKey);
            
            await _rightRepository.UpdateAsync(right);
        }

        public async Task DeleteRightAsync(int rightID)
        {
            _memoryCache.Remove(RightDefaults.RightAllCacheKey);
            _memoryCache.Remove(RightDefaults.RightByIdCacheKey);
            
            await _rightRepository.DeleteAsync(rightID);
        }

        #endregion
    }
}