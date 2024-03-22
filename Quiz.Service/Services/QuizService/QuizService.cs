using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class QuizService : IQuizService
    {
        #region properties
        
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<AnswerType> _answerTypeRepository;
        private readonly IRepository<QuizTheme> _quizThemeRepository;
        private readonly IRepository<QuestionType> _questionTypeRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<ExamType> _examTypeRepository;
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor
        
        public QuizService(IRepository<Quiz> quizRepository, IRepository<QuizTheme> quizThemeRepository,
            IRepository<QuestionType> questionTypesRepository, IRepository<Question> questionRepository,
            IRepository<ExamType> examTypeReository,
            IRepository<AnswerType> answerTypeRepository,IMemoryCache memoryCache)
        {
            _answerTypeRepository = answerTypeRepository;
            _quizRepository = quizRepository;
            _quizThemeRepository = quizThemeRepository;
            _questionTypeRepository = questionTypesRepository;
            _questionRepository = questionRepository;
            _examTypeRepository = examTypeReository;

            _memoryCache = memoryCache;
        }

        #endregion

        #region methods 

        public Quiz GetQuizByID(int quizID)
        {
            return _quizRepository.GetById(quizID);
        }

        public List<Quiz> GetAllQuizes()
        {
            if (_memoryCache.TryGetValue(QuizDefaults.QuizAllCacheKey, out List<Quiz> quizzes)) 
                return quizzes.ToList();
                
            quizzes = _quizRepository.Table.OrderBy(k => k.QuizName).ToList();
            _memoryCache.Set(QuizDefaults.QuizAllCacheKey, quizzes);

            return quizzes.ToList();
        }

        public void UpdateQuiz(Quiz quiz)
        {
            _memoryCache.Remove(QuizDefaults.QuizAllCacheKey);
            _memoryCache.Remove(QuizDefaults.QuizIdCacheKey);
            
            _quizRepository.Update(quiz);
        }

        public void AddQuiz(Quiz quiz)
        {
            _memoryCache.Remove(QuizDefaults.QuizAllCacheKey);
            _memoryCache.Remove(QuizDefaults.QuizIdCacheKey);
            
            _quizRepository.Insert(quiz);
        }

        public void DeleteQuiz(int quizID)
        {
            _memoryCache.Remove(QuizDefaults.QuizAllCacheKey);
            _memoryCache.Remove(QuizDefaults.QuizIdCacheKey);
            
            _quizRepository.Delete(quizID);
        }

        #endregion
        
        #region async methods
        
        public async Task<List<Quiz>> GetAllQuizesAsync()
        {
            if (_memoryCache.TryGetValue(QuizDefaults.QuizAllCacheKey, out List<Quiz> quizzes)) 
                return quizzes.ToList();
                
            quizzes = await _quizRepository.Table.OrderBy(k => k.QuizName).ToListAsync();
            _memoryCache.Set(QuizDefaults.QuizAllCacheKey, quizzes);

            return quizzes.ToList();
        }

        public async Task<Quiz> GetQuizByIDAsync(int quizID)
        {
            return await _quizRepository.GetByIdAsync(quizID);
        }

        public async Task AddQuizAsync(Quiz quiz)
        {
            _memoryCache.Remove(QuizDefaults.QuizAllCacheKey);
            _memoryCache.Remove(QuizDefaults.QuizIdCacheKey);
            
            await _quizRepository.InsertAsync(quiz);
        }

        public async Task UpdateQuizAsync(Quiz quiz)
        {
            _memoryCache.Remove(QuizDefaults.QuizAllCacheKey);
            _memoryCache.Remove(QuizDefaults.QuizIdCacheKey);
            
            await _quizRepository.UpdateAsync(quiz);
        }

        public async Task DeleteQuizAsync(int quizID)
        {
            _memoryCache.Remove(QuizDefaults.QuizAllCacheKey);
            _memoryCache.Remove(QuizDefaults.QuizIdCacheKey);
            
            await _quizRepository.DeleteAsync(quizID);
        }

        #endregion

        #region other

        public List<QuizSummary> GetQuizSummary(int quizID, int questionTypeID)
        {
            var result = (from quizzes in _quizRepository.Table
                          join quizThemes in _quizThemeRepository.Table on quizzes.ID equals quizThemes.QuizID
                          join questionTypes in _questionTypeRepository.Table on quizzes.ID equals questionTypes.QuizID
                          join answerTypes in _answerTypeRepository.Table on questionTypes.ID equals answerTypes.QuestionTypeID into tmp
                          from answerTypes in tmp.DefaultIfEmpty()
                          where (quizzes.ID == quizID || quizID == 0) && (questionTypes.ID == questionTypeID || questionTypeID == 0)
                          select new QuizSummary
                          {
                              ID = quizzes.ID,
                              QuizThemeID = quizThemes.ID,
                              QuestionTypeID = questionTypes.ID,
                              AnswerTypeID = answerTypes == null ? 0 : answerTypes.ID,
                              QuizName = quizzes.QuizName,
                              QuizThemeName = quizThemes.QuizThemeName,
                              QuestionTypeName = questionTypes.QuestionTypeName,
                              AnswerTypeName = answerTypes == null ? "" : answerTypes.AnswerTypeName
                          }).OrderBy(k => k.QuizName).ToList();

            return result;
        }

        public async Task<List<Quiz>> GetAllQuizzesWithChild()
        {
            var quizzes = _quizRepository.Table
                .Include(p => p.QuizThemes)
                .Include(p => p.QuestionTypes)
                .Include(p => p.Questions)
                .Include(p => p.AnswerTypes)
                .OrderBy(k => k.QuizName)
                .ToListAsync();

            return  await quizzes;
        }

        public async Task<List<QuizThemeSummary>> GetAllQuizThemesByQuizIDAsync(int quizID)
        {
            var result = (from quizzes in _quizRepository.Table
                          join quizThemes in _quizThemeRepository.Table on quizzes.ID equals quizThemes.QuizID
                          where (quizzes.ID == quizID || quizID == 0)
                          select new QuizThemeSummary
                          {
                              ID = quizThemes.ID,
                              QuizID = quizzes.ID,
                              QuizName = quizzes.QuizName,
                              QuizThemeName = quizThemes.QuizThemeName,
                          }).OrderBy(k => k.QuizName).ToListAsync();

            return await result;
        }

        public async Task<List<QuestionSummary>> GetAllQuestionsByQuizThemesAsync(int quizID, List<int> quizThemeIDs)
        {
            var result = (from questions in _questionRepository.Table
                          join quizes in _quizRepository.Table on questions.QuizID equals quizes.ID
                          join quizThemes in _quizThemeRepository.Table on questions.QuizThemeID equals quizThemes.ID
                          where quizes.ID == quizID && (quizThemeIDs.Contains(quizThemes.ID) || quizThemeIDs.Count == 0)
                          select new QuestionSummary
                          {
                              ID = questions.ID,
                              QuizID = quizes.ID,
                              QuizThemeID = quizThemes.ID,
                              QuestionTypeID = questions.QuestionTypeID,
                              AnswerTypeID = questions.AnswerTypeID,
                              QuestionText = questions.QuestionText,
                              QuizName = quizes.QuizName,
                              QuizThemeName = quizThemes.QuizThemeName,
                              //CorrectAnswer = questions.CorrectAnswer
                          }).OrderBy(k => k.QuizThemeName).ToListAsync();

            return await result;
        }

        public async Task<List<QuestionSummary>> GetAllQuestionsByExamTypeAsync(int quizID, int examTypeID)
        {
            var examType = await _examTypeRepository.GetByIdAsync(examTypeID);
            var extendedDataElement = examType.ExtendedDataElement; 
            var questions = GetAllQuestionsByQuizThemesAsync(quizID, new List<int>());

            return await questions;
        }

        #endregion
    }
}