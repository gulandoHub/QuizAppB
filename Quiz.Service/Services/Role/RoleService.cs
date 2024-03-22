using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class RoleService : IRoleService
    {
        #region properties

        private readonly IRepository<Role> _roleRepository;
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor
        
        public RoleService(IRepository<Role> roleRepository, IMemoryCache memoryCache)
        {
            _roleRepository = roleRepository;
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods
        
        public List<Role> GetAllRoles()
        {
            if (_memoryCache.TryGetValue(RoleDefaults.RoleAllCacheKey, out List<Role> roles)) 
                return roles.ToList();
                
            roles = _roleRepository.Table.ToList();
            _memoryCache.Set(RoleDefaults.RoleAllCacheKey, roles);

            return roles.ToList();
        }

        public Role GetRoleByID(int roleID)
        {
            return _roleRepository.GetById(roleID);
        }

        public void UpdateRole(Role role)
        {
            _memoryCache.Remove(RoleDefaults.RoleAllCacheKey);
            _memoryCache.Remove(RoleDefaults.RoleByIdCacheKey);
            
            _roleRepository.Update(role);
        }

        public void AddRole(Role role)
        {
            _memoryCache.Remove(RoleDefaults.RoleAllCacheKey);
            _memoryCache.Remove(RoleDefaults.RoleByIdCacheKey);
            
            _roleRepository.Insert(role);
        }

        public void DeleteRole(int roleID)
        {
            _memoryCache.Remove(RoleDefaults.RoleAllCacheKey);
            _memoryCache.Remove(RoleDefaults.RoleByIdCacheKey);
            
            _roleRepository.Delete(roleID);
        }

        #endregion
        
        #region async methods
        
        public async Task<List<Role>> GetAllRolesAsync()
        {
            if (_memoryCache.TryGetValue(RoleDefaults.RoleAllCacheKey, out List<Role> roles)) 
                return roles.ToList();
                
            roles = await _roleRepository.Table.ToListAsync();
            _memoryCache.Set(RoleDefaults.RoleAllCacheKey, roles);

            return roles.ToList();
        }

        public async Task<Role> GetRoleByIDAsync(int roleID)
        {
            return await _roleRepository.GetByIdAsync(roleID);
        }

        public async Task AddRoleAsync(Role role)
        {
            _memoryCache.Remove(RoleDefaults.RoleAllCacheKey);
            _memoryCache.Remove(RoleDefaults.RoleByIdCacheKey);
            
            await _roleRepository.InsertAsync(role);
        }

        public async Task UpdateRoleAsync(Role role)
        {
            _memoryCache.Remove(RoleDefaults.RoleAllCacheKey);
            _memoryCache.Remove(RoleDefaults.RoleByIdCacheKey);
            
            await _roleRepository.UpdateAsync(role);
        }

        public async Task DeleteRoleAsync(int roleID)
        {
            _memoryCache.Remove(RoleDefaults.RoleAllCacheKey);
            _memoryCache.Remove(RoleDefaults.RoleByIdCacheKey);
            
            await _roleRepository.DeleteAsync(roleID);
        }

        #endregion
    }
}