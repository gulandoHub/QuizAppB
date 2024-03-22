using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IUserService
    {
        #region basic methods
        
        List<User> GetAllUsers();

        User GetUserByID(int userID);

        void AddUser(User user);
        
        void UpdateUser(User user);

        void DeleteUser(int userID);
        
        #endregion
        
        #region async basic methods
        
        Task<List<User>> GetAllUsersAsync();

        Task<User> GetUserByIDAsync(int userID);

        Task AddUserAsync(User user);
        
        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(int userID);
        
        #endregion

        #region api async methods

        Task<User> Authenticate(string username, string password);
        
        Task Create(User user, string password);
        
        Task Update(User user, string password = null);
        
        #endregion
    }
}