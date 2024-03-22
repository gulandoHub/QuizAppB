using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class RoleRightService : IRoleRightService
    {
        #region properties
        
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Right> _rightRepository;
        private readonly IRepository<RoleRight> _roleRightRepository;
        
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public RoleRightService(IRepository<Role> roleRepository, IRepository<Right> rightRepository,
            IRepository<RoleRight> roleRightRepository,IMemoryCache memoryCache)
        {
            _roleRepository = roleRepository;
            _rightRepository = rightRepository;
            _roleRightRepository = roleRightRepository;
            
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods

        public List<RoleRight> GetAllRoleRights()
        {
            if (_memoryCache.TryGetValue(RoleRightDefaults.RoleRightAllCacheKey, out List<RoleRight> roleRights)) 
                return roleRights.ToList();
                
            roleRights = _roleRightRepository.Table.ToList();
            _memoryCache.Set(RoleRightDefaults.RoleRightAllCacheKey, roleRights);

            return roleRights;
        }

        public RoleRight GetRoleRightByID(int roleRightID)
        {
            return _roleRightRepository.GetById(roleRightID);
        }

        public void UpdateRoleRight(RoleRight roleRight)
        {
            _memoryCache.Remove(RoleRightDefaults.RoleRightAllCacheKey);
            _memoryCache.Remove(RoleRightDefaults.RoleRightByIdCacheKey);
            
            _roleRightRepository.Update(roleRight);
        }

        public void AddRoleRight(RoleRight roleRight)
        {
            _memoryCache.Remove(RoleRightDefaults.RoleRightAllCacheKey);
            _memoryCache.Remove(RoleRightDefaults.RoleRightByIdCacheKey);
            
            _roleRightRepository.Insert(roleRight);
        }

        public void DeleteRoleRight(int roleRightID)
        {
            _memoryCache.Remove(RoleRightDefaults.RoleRightAllCacheKey);
            _memoryCache.Remove(RoleRightDefaults.RoleRightByIdCacheKey);
            
            _roleRightRepository.Delete(roleRightID);
        }

        public List<RoleRightSummary> GetRoleRightSummary(int roleRightID = 0)
        {
            var result = (from roleRights in _roleRightRepository.Table
                join roles in _roleRepository.Table on roleRights.RoleID equals roles.ID
                join rights in _rightRepository.Table on roleRights.RightID equals rights.ID
                select new RoleRightSummary
                {
                    ID = roleRights.ID,
                    RoleID = roles.ID,
                    RightID = rights.ID,
                    RoleName = roles.Name,
                    RightName = rights.Name
                }).ToList();

            return result;     
        }
        
        #endregion

        #region async methods
        
        public async Task<List<RoleRight>> GetAllRoleRightsAsync()
        {
            if (_memoryCache.TryGetValue(RoleRightDefaults.RoleRightAllCacheKey, out List<RoleRight> roleRights)) 
                return roleRights.ToList();
                
            roleRights = await _roleRightRepository.Table.ToListAsync();
            _memoryCache.Set(RoleRightDefaults.RoleRightAllCacheKey, roleRights);

            return roleRights;
        }

        public async Task<List<RoleRightSummary>> GetRoleRightSummaryAsync(int roleRightID = 0)
        {
            var result = (from roleRights in _roleRightRepository.Table
                join roles in _roleRepository.Table on roleRights.RoleID equals roles.ID
                join rights in _rightRepository.Table on roleRights.RightID equals rights.ID
                select new RoleRightSummary
                {
                    ID = roleRights.ID,
                    RoleID = roles.ID,
                    RightID = rights.ID,
                    RoleName = roles.Name,
                    RightName = rights.Name
                }).ToListAsync();

            return await result;  
        }

        public async Task<RoleRight> GetRoleRightByIDAsync(int roleRightID)
        {
            return await _roleRightRepository.GetByIdAsync(roleRightID);
        }

        public async Task AddRoleRightAsync(RoleRight roleRight)
        {
            _memoryCache.Remove(RoleRightDefaults.RoleRightAllCacheKey);
            _memoryCache.Remove(RoleRightDefaults.RoleRightByIdCacheKey);
            
            await _roleRightRepository.InsertAsync(roleRight);
        }

        public async Task UpdateRoleRightAsync(RoleRight roleRight)
        {
            _memoryCache.Remove(RoleRightDefaults.RoleRightAllCacheKey);
            _memoryCache.Remove(RoleRightDefaults.RoleRightByIdCacheKey);
            
            await _roleRightRepository.UpdateAsync(roleRight);
        }

        public async Task DeleteRoleRightAsync(int roleRightID)
        {
            _memoryCache.Remove(RoleRightDefaults.RoleRightAllCacheKey);
            _memoryCache.Remove(RoleRightDefaults.RoleRightByIdCacheKey);
            
            await _roleRightRepository.DeleteAsync(roleRightID);
        }

        #endregion
    }
}