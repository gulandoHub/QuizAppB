using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IQuestionTypeService
    {
        #region methods
        
        List<QuestionType> GetAllQuestionTypes();

        List<QuestionTypeSummary> GetQuestionTypeSummary(int questionTypeID = 0, int quizID = 0);
        
        QuestionType GetQuestionTypeByID(int questionTypeID);

        void AddQuestionType(QuestionType questionType);
        
        void UpdateQuestionType(QuestionType questionType);

        void DeleteQuestionType(int questionTypeID);
        
        #endregion
        
        #region methods async
        
        Task<List<QuestionType>> GetAllQuestionTypesAsync();
        
        Task<List<QuestionTypeSummary>> GetQuestionTypeSummaryAsync(int questionTypeID = 0);

        Task<QuestionType> GetQuestionTypeByIDAsync(int questionTypeID);

        Task AddQuestionTypeAsync(QuestionType questionType);
        
        Task UpdateQuestionTypeAsync(QuestionType questionType);

        Task DeleteQuestionTypeAsync(int questionTypeID);
        
        #endregion


    }
}