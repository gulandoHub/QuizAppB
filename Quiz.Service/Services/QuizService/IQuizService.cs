using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IQuizService
    {
        #region methods
        
        List<Quiz> GetAllQuizes();

        Quiz GetQuizByID(int quizID);

        void UpdateQuiz(Quiz quiz);

        void AddQuiz(Quiz quiz);

        void DeleteQuiz(int quizID);
        
        List<QuizSummary> GetQuizSummary(int quizID, int questionTypeID);
        
        #endregion
        
        #region methods async
        
        Task<List<Quiz>> GetAllQuizesAsync();

        Task<Quiz> GetQuizByIDAsync(int quizID);

        Task AddQuizAsync(Quiz quiz);
        
        Task UpdateQuizAsync(Quiz quiz);

        Task DeleteQuizAsync(int quizID);

        #endregion

        #region other 

        Task<List<Quiz>> GetAllQuizzesWithChild();

        Task<List<QuizThemeSummary>> GetAllQuizThemesByQuizIDAsync(int quizID);

        Task<List<QuestionSummary>> GetAllQuestionsByQuizThemesAsync(int quizID, List<int> quizThemeIDs);

        Task<List<QuestionSummary>> GetAllQuestionsByExamTypeAsync(int quizID, int examType);

        #endregion

    }
}