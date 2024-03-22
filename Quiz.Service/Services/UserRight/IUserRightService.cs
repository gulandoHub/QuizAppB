using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IUserRightService
    {
        #region methods
        
        List<UserRight> GetAllUserRights();
        
        List<UserRightSummary> GetUserRightSummary(int userRightID = 0);
        
        UserRight GetUserRightByID(int userRightID);

        void UpdateUserRight(UserRight userRight);

        void AddUserRight(UserRight userRight);

        void DeleteUserRight(int userRightID);
        
        #endregion
        
        #region async methods
        
        Task<List<UserRight>> GetAllUserRightsAsync();

        Task<List<UserRightSummary>> GetUserRightSummaryAsync(int userRightID = 0);
        
        Task<UserRight> GetUserRightByIDAsync(int userRightID);

        Task AddUserRightAsync(UserRight userRight);
        
        Task UpdateUserRightAsync(UserRight userRight);

        Task DeleteUserRightAsync(int userRightID);
        
        #endregion
    }
}