using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class QuestionTypeService : IQuestionTypeService
    {
        #region properties
        
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<QuestionType> _questionTypesRepository; 
        
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public QuestionTypeService(IRepository<Quiz> quizRepository, IRepository<QuestionType> questionTypesRepository,
            IMemoryCache memoryCache)
        {
            _quizRepository = quizRepository;
            _questionTypesRepository = questionTypesRepository;
            
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods
        
        public List<QuestionType> GetAllQuestionTypes()
        {
            if (_memoryCache.TryGetValue(QuestionTypeDefaults.QuestionTypeAllCacheKey, out List<QuestionType> questionTypes))
                return questionTypes;

            questionTypes = _questionTypesRepository.Table.OrderBy(k => k.QuestionTypeName).ToList();
            _memoryCache.Set(QuestionTypeDefaults.QuestionTypeAllCacheKey, questionTypes);
    
            return questionTypes;
        }

        public List<QuestionTypeSummary> GetQuestionTypeSummary(int questionTypeID = 0, int quizID = 0)
        {
            var result = (from questionTypes in _questionTypesRepository.Table
                join quizes in _quizRepository.Table on questionTypes.QuizID equals quizes.ID
                orderby quizes.QuizName
                where (questionTypes.ID == questionTypeID || questionTypeID == 0) && (quizes.ID == quizID || quizID == 0)
                select new QuestionTypeSummary
                {
                    ID = questionTypes.ID,
                    QuizID = quizes.ID,
                    QuizName = quizes.QuizName,
                    QuestionTypeName = questionTypes.QuestionTypeName
                }).OrderBy(k => k.QuestionTypeName).ToList();

            return result;     
        }
        
        public QuestionType GetQuestionTypeByID(int questionTypeID)
        {
            return _questionTypesRepository.GetById(questionTypeID);
        }

        public void UpdateQuestionType(QuestionType questionType)
        {
            _memoryCache.Remove(QuestionTypeDefaults.QuestionTypeAllCacheKey);
            _memoryCache.Remove(QuestionTypeDefaults.QuestionTypeIdCacheKey);
            
           _questionTypesRepository.Update(questionType);
        }

        public void AddQuestionType(QuestionType questionType)
        {
            _memoryCache.Remove(QuestionTypeDefaults.QuestionTypeAllCacheKey);
            _memoryCache.Remove(QuestionTypeDefaults.QuestionTypeIdCacheKey);
            
            _questionTypesRepository.Insert(questionType);
        }

        public void DeleteQuestionType(int questionTypeID)
        {
            _memoryCache.Remove(QuestionTypeDefaults.QuestionTypeAllCacheKey);
            _memoryCache.Remove(QuestionTypeDefaults.QuestionTypeIdCacheKey);
            
            _questionTypesRepository.Delete(questionTypeID);
        }

        #endregion
        
        #region async methods
        
        public async Task<List<QuestionType>> GetAllQuestionTypesAsync()
        {
            if (_memoryCache.TryGetValue(QuestionTypeDefaults.QuestionTypeAllCacheKey, out List<QuestionType> questionTypes))
                return questionTypes;

            questionTypes = await _questionTypesRepository.Table.OrderBy(k => k.QuestionTypeName).ToListAsync();
            _memoryCache.Set(QuestionTypeDefaults.QuestionTypeAllCacheKey, questionTypes);
    
            return questionTypes;
        }

        public async Task<List<QuestionTypeSummary>> GetQuestionTypeSummaryAsync(int questionTypeID = 0)
        {
            var result = (from questionTypes in _questionTypesRepository.Table
                join quizes in _quizRepository.Table on questionTypes.QuizID equals quizes.ID
                orderby quizes.QuizName
                where questionTypes.ID == questionTypeID || questionTypeID == 0
                select new QuestionTypeSummary
                {
                    ID = questionTypes.ID,
                    QuizID = quizes.ID,
                    QuizName = quizes.QuizName,
                    QuestionTypeName = questionTypes.QuestionTypeName
                }).OrderBy(k => k.QuestionTypeName).ToListAsync();

            return await result;   
        }

        public async Task<QuestionType> GetQuestionTypeByIDAsync(int questionTypeID)
        {
            return await _questionTypesRepository.GetByIdAsync(questionTypeID);
        }

        public async Task AddQuestionTypeAsync(QuestionType questionType)
        {
            _memoryCache.Remove(QuestionTypeDefaults.QuestionTypeAllCacheKey);
            _memoryCache.Remove(QuestionTypeDefaults.QuestionTypeIdCacheKey);
            
            await _questionTypesRepository.InsertAsync(questionType);
        }

        public async Task UpdateQuestionTypeAsync(QuestionType questionType)
        {
            _memoryCache.Remove(QuestionTypeDefaults.QuestionTypeAllCacheKey);
            _memoryCache.Remove(QuestionTypeDefaults.QuestionTypeIdCacheKey);
            
            await _questionTypesRepository.UpdateAsync(questionType);
        }

        public async Task DeleteQuestionTypeAsync(int questionTypeID)
        {
            _memoryCache.Remove(QuestionTypeDefaults.QuestionTypeAllCacheKey);
            _memoryCache.Remove(QuestionTypeDefaults.QuestionTypeIdCacheKey);
            
            await _questionTypesRepository.DeleteAsync(questionTypeID);
        }
        
        #endregion
    }
}