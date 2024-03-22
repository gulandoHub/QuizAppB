using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IRoleService 
    {
        #region methods
        
        List<Role> GetAllRoles();

        Role GetRoleByID(int roleID);

        void UpdateRole(Role role);

        void AddRole(Role role);

        void DeleteRole(int roleID);
        
        #endregion
        
        #region async methods
        
        Task<List<Role>> GetAllRolesAsync();

        Task<Role> GetRoleByIDAsync(int roleID);

        Task AddRoleAsync(Role role);
        
        Task UpdateRoleAsync(Role role);

        Task DeleteRoleAsync(int roleID);
        
        #endregion
        
    }
}