using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;


namespace QuizService
{
    public interface IAnswerTypeService
    {
        #region methods
        
        List<AnswerType> GetAllAnswerTypes();

        AnswerType GetAnswerTypeByID(int answerTypeID);

        void UpdateAnswerType(AnswerType answerType);

        void AddAnswerType(AnswerType answerType);

        void DeleteAnswerType(int answerID);
        
        List<AnswerTypeSummary> GetAnswerTypeSummary(int answerTypeID = 0);

        AnswerType GetAnswerTypeByQuestionType(int questionTypeID);

        #endregion

        #region async methods

        Task<List<AnswerType>> GetAllAnswerTypesAsync();

        Task<AnswerType> GetAnswerTypeByIDAsync(int answerTypeID);

        Task AddAnswerTypeAsync(AnswerType answerType);
        
        Task UpdateAnswerTypeAsync(AnswerType answerType);

        Task DeleteAnswerTypeAsync(int answerTypeID);

        Task<List<AnswerTypeSummary>> GetAnswerTypeSummaryAsync(int answerTypeID = 0);
        
        #endregion

    }
}