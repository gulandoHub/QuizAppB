using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IRightService
    {
        #region methods
        
        List<Right> GetAllRights();

        Right GetRightByID(int rightID);

        void UpdateRight(Right right);

        void AddRight(Right right);

        void DeleteRight(int rightID);

        #endregion
        
        #region async methods
        
        Task<List<Right>> GetAllRightsAsync();

        Task<Right> GetRightByIDAsync(int rightID);

        Task AddRightAsync(Right right);
        
        Task UpdateRightAsync(Right right);

        Task DeleteRightAsync(int rightID);
        
        #endregion
    }
}