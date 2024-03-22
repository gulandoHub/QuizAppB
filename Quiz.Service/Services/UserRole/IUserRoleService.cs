using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;


namespace QuizService
{
    public interface IUserRoleService
    {
        #region methods
        
        List<UserRole> GetAllUserRoles();
        
        List<UserRoleSummary> GetUserRoleSummary(int userRoleID = 0);
        
        UserRole GetUserRoleByID(int userRoleID);

        void UpdateUserRole(UserRole userRole);

        void AddUserRole(UserRole userRole);

        void DeleteUserRole(int userRoleID);
        
        #endregion

        #region async methods

        Task<List<UserRole>> GetAllUserRolesAsync();

        Task<List<UserRoleSummary>> GetUserRoleSummaryAsync(int userRoleID = 0);
        
        Task<UserRole> GetUserRoleByIDAsync(int userRoleID);

        Task AddUserRoleAsync(UserRole userRole);
        
        Task UpdateUserRoleAsync(UserRole userRole);

        Task DeleteUserRoleAsync(int userRoleID);

        #endregion

    }
}