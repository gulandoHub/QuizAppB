using QuizData;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace QuizService
{
    public interface IExamTypeService
    {
        #region methods

        List<ExamType> GetAllExamTypes();

        ExamType GetExamTypeByID(int ExamTypeID);

        void UpdateExamType(ExamType ExamType);

        void AddExamType(ExamType examType);

        void DeleteExamType(int examTypeID);

        #endregion


        #region async methods

        Task<List<ExamType>> GetAllExamTypesAsync();

        Task<ExamType> GetExamTypeByIDAsync(int examTypeID);

        Task AddExamTypeAsync(ExamType ExamType);

        Task UpdateExamTypeAsync(ExamType ExamType);

        Task DeleteExamTypeAsync(int examTypeID);

        #endregion
    }
}
