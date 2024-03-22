using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IAnswerService
    {
        #region methods
        
        List<Answer> GetAllAnswers();

        Answer GetAnswerByID(int answerID);

        void UpdateAnswer(Answer answer);

        void AddAnswer(Answer answer);

        void DeleteAnswer(int answerID);

        List<AnswerSummary> GetAnswerSummary(int answerID = 0);
        
        #endregion
        
        #region async methods
        
        Task<List<Answer>> GetAllAnswersAsync();

        Task<Answer> GetAnswerByIDAsync(int answerID);

        Task AddAnswerAsync(Answer answer);
        
        Task UpdateAnswerAsync(Answer answer);

        Task DeleteAnswerAsync(int answerID);

        Task<List<AnswerSummary>> GetAnswerSummaryAsync(int answerID = 0);
        
        #endregion
    }
}