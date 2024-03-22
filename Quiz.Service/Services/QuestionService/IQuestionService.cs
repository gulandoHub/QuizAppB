using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;


namespace QuizService
{
    public interface IQuestionService
    {
        #region methods
        
        List<Question> GetAllQuestions();
        
        List<QuestionSummary> GetQuestionSummary(int questionID = 0);

        Question GetQuestionByID(int questionID);

        void UpdateQuestion(Question question);

        int AddQuestion(Question question);

        void DeleteQuestion(int questionID);
        
        #endregion
        
        #region async methods
        
        Task<List<Question>> GetAllQuestionsAsync();
        
        Task<List<QuestionSummary>> GetQuestionSummaryAsync(int questionID = 0);

        Task<Question> GetQuestionByIDAsync(int questionID);

        Task AddQuestionAsync(Question question);
        
        Task UpdateQuestionAsync(Question question);

        Task DeleteQuestionAsync(int questionID);
        
        #endregion
    }
}