using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;

namespace QuizService
{
    public class ExamTypeService : IExamTypeService
    {
        #region properties

        private readonly IRepository<ExamType> _examTypeRepository;
        private readonly IMemoryCache _memoryCache;

        #endregion

        #region ctor

        public ExamTypeService(IRepository<ExamType> examTypeRepository, IMemoryCache memoryCache)
        {
            _examTypeRepository = examTypeRepository;
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods

        public List<ExamType> GetAllExamTypes()
        {
            return _examTypeRepository.Table.OrderBy(k => k.ExamTypeName).ToList();
        }

        public ExamType GetExamTypeByID(int ExamTypeID)
        {
            throw new NotImplementedException();
        }

        public void AddExamType(ExamType examType)
        {
            throw new NotImplementedException();
        }

        public void UpdateExamType(ExamType ExamType)
        {
            throw new NotImplementedException();
        }

        public void DeleteExamType(int examTypeID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region async methods

        public async Task<List<ExamType>> GetAllExamTypesAsync()
        {
            return await _examTypeRepository.Table.OrderBy(k => k.ExamTypeName).ToListAsync();
        }

        public async Task<ExamType> GetExamTypeByIDAsync(int examTypeID)
        {
            return await _examTypeRepository.GetByIdAsync(examTypeID);
        }

        public async Task AddExamTypeAsync(ExamType ExamType)
        {
            await _examTypeRepository.InsertAsync(ExamType);
        }

        public async Task DeleteExamTypeAsync(int examTypeID)
        {
            await _examTypeRepository.DeleteAsync(examTypeID);
        }

        public async Task UpdateExamTypeAsync(ExamType ExamType)
        {
            await _examTypeRepository.UpdateAsync(ExamType);
        }

        #endregion
    }
}
