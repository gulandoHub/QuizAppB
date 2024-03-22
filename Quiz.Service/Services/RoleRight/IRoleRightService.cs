using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IRoleRightService
    {
        #region methods
        
        List<RoleRight> GetAllRoleRights();

        RoleRight GetRoleRightByID(int roleRightID);

        void UpdateRoleRight(RoleRight roleRight);

        void AddRoleRight(RoleRight roleRight);

        void DeleteRoleRight(int roleRightID);
        
        List<RoleRightSummary> GetRoleRightSummary(int roleRightID = 0);
        
        #endregion
        
        #region async methods
        
        Task<List<RoleRight>> GetAllRoleRightsAsync();

        Task<List<RoleRightSummary>> GetRoleRightSummaryAsync(int roleRightID = 0);
        
        Task<RoleRight> GetRoleRightByIDAsync(int roleRightID);

        Task AddRoleRightAsync(RoleRight roleRight);
        
        Task UpdateRoleRightAsync(RoleRight roleRight);

        Task DeleteRoleRightAsync(int roleRightID);
        
        #endregion
    }
}