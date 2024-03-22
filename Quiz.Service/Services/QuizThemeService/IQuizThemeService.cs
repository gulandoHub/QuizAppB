using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IQuizThemeService
    {
        #region methods
        
        List<QuizTheme> GetAllQuizThemes();

        List<QuizThemeSummary> GetQuizThemeSummary(int quizThemeID = 0);
        
        QuizTheme GetQuizThemeByID(int quizThemeID);

        void UpdateQuizTheme(QuizTheme quizTheme);

        void AddQuizTheme(QuizTheme quizTheme);

        void DeleteQuizTheme(int quizThemeID);
        
        #endregion
        
        #region async methods
        
        Task<List<QuizTheme>> GetAllQuizThemesAsync();

        Task<List<QuizThemeSummary>> GetQuizThemeSummaryAsync(int quizThemeID = 0);
        
        Task<QuizTheme> GetQuizThemeByIDAsync(int quizThemeID);

        Task AddQuizThemeAsync(QuizTheme quizTheme);
        
        Task UpdateQuizThemeAsync(QuizTheme quizTheme);

        Task DeleteQuizThemeAsync(int quizThemeID);
        
        #endregion
    }
}