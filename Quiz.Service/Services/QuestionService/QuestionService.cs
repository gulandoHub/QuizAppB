using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class QuestionService : IQuestionService
    {
        #region properties
        
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<QuizTheme> _quizThemeRepository;
        private readonly IRepository<AnswerType> _answerTypeRepository;
        private readonly IRepository<QuestionType> _questionTypesRepository;
        private readonly IRepository<Image> _imageRepository;
        private readonly IRepository<ExamType> _examTypeRepository;

        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public QuestionService(IRepository<Quiz> quizRepository, IRepository<QuizTheme> quizThemeRepository,
            IRepository<AnswerType> answerTypeRepository, IRepository<QuestionType> questionTypesRepository,
            IRepository<Question> questionRepository,IMemoryCache memoryCache, IRepository<Image> imageRepository, IRepository<ExamType> examTypeRepository)
        {
            _questionRepository = questionRepository;
            _quizRepository = quizRepository;
            _quizThemeRepository = quizThemeRepository;
            _answerTypeRepository = answerTypeRepository;
            _questionTypesRepository = questionTypesRepository;
            _imageRepository = imageRepository;
            _examTypeRepository = examTypeRepository;

            _memoryCache = memoryCache;
        }

        #endregion

        #region methods
        
        public List<Question> GetAllQuestions()
        {
            if (_memoryCache.TryGetValue(QuestionDefaults.QuestionAllCacheKey, out List<Question> questions))
                return questions;

            questions = _questionRepository.Table.ToList();
            _memoryCache.Set(QuestionDefaults.QuestionAllCacheKey, questions);
    
            return questions;
        }

        public Question GetQuestionByID(int questionID)
        {
            return _questionRepository.GetById(questionID);
        }

        public List<QuestionSummary> GetQuestionSummary(int questionID = 0)
        {
            var result = (from questions in _questionRepository.Table
                join quizes in _quizRepository.Table on questions.QuizID equals quizes.ID
                join quizThemes in _quizThemeRepository.Table on questions.QuizThemeID equals quizThemes.ID
                join answerTypes in _answerTypeRepository.Table on questions.AnswerTypeID equals answerTypes.ID
                join questionTypes in _questionTypesRepository.Table on questions.QuestionTypeID equals questionTypes.ID
                //join images in _imageRepository.Table on questions.ID equals images.QuestionID
                where questions.ID == questionID || questionID == 0
                select new QuestionSummary 
                {
                    ID = questions.ID,
                    QuizID = quizes.ID,
                    QuizThemeID =  quizThemes.ID,
                    QuestionTypeID = questionTypes.ID,
                    AnswerTypeID = answerTypes.ID,
                    QuestionNo = questions.QuestionNo,
                    //ImageID = images.ID,
                    QuestionText = questions.QuestionText,
                    QuizName = quizes.QuizName,
                    QuizThemeName = quizThemes.QuizThemeName,
                    QuestionTypeName = questionTypes.QuestionTypeName,
                    AnswerTypeName = answerTypes.AnswerTypeName,
                    CorrectAnswer = questions.CorrectAnswer
                }).ToList();

            return result;     
        }
        
        public void UpdateQuestion(Question question)
        {
            _memoryCache.Remove(QuestionDefaults.QuestionAllCacheKey);
            _memoryCache.Remove(QuestionDefaults.QuestionyIdCacheKey);
            
            _questionRepository.Update(question);
        }

        public int AddQuestion(Question question)
        {
            _memoryCache.Remove(QuestionDefaults.QuestionAllCacheKey);
            _memoryCache.Remove(QuestionDefaults.QuestionyIdCacheKey);
            
            return _questionRepository.InsertGetID(question);
        }

        public void DeleteQuestion(int questionID)
        {
            _memoryCache.Remove(QuestionDefaults.QuestionAllCacheKey);
            _memoryCache.Remove(QuestionDefaults.QuestionyIdCacheKey);
            
            _questionRepository.Delete(questionID);
        }

        #endregion
        
        #region async methods
        
        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            if (_memoryCache.TryGetValue(QuestionDefaults.QuestionAllCacheKey, out List<Question> questions))
                return questions;

            questions = await _questionRepository.Table.ToListAsync();
            _memoryCache.Set(QuestionDefaults.QuestionAllCacheKey, questions);
    
            return questions;
        }

        public async Task<List<QuestionSummary>> GetQuestionSummaryAsync(int questionID = 0)
        {
            var result = (from questions in _questionRepository.Table
                join quizes in _quizRepository.Table on questions.QuizID equals quizes.ID
                join quizThemes in _quizThemeRepository.Table on questions.QuizThemeID equals quizThemes.ID
                join answerTypes in _answerTypeRepository.Table on questions.AnswerTypeID equals answerTypes.ID
                join questionTypes in _questionTypesRepository.Table on questions.QuestionTypeID equals questionTypes.ID
                //join images in _imageRepository.Table on questions.ID equals images.QuestionID
                where questions.ID == questionID || questionID == 0
                select new QuestionSummary 
                {
                    ID = questions.ID,
                    QuizID = quizes.ID,
                    QuizThemeID =  quizThemes.ID,
                    QuestionTypeID = questionTypes.ID,
                    AnswerTypeID = answerTypes.ID,
                    QuestionNo = questions.QuestionNo,
                    //ImageID = images.ID,
                    //ImageData = images.Data,
                    QuestionText = questions.QuestionText,
                    QuizName = quizes.QuizName,
                    QuizThemeName = quizThemes.QuizThemeName,
                    QuestionTypeName = questionTypes.QuestionTypeName,
                    AnswerTypeName = answerTypes.AnswerTypeName,
                    CorrectAnswer = questions.CorrectAnswer
                }). ToListAsync();

            return await result;    
        }

        public async Task<Question> GetQuestionByIDAsync(int questionID)
        {
            return await _questionRepository.GetByIdAsync(questionID);
        }

        public async Task AddQuestionAsync(Question question)
        {
            _memoryCache.Remove(QuestionDefaults.QuestionAllCacheKey);
            _memoryCache.Remove(QuestionDefaults.QuestionyIdCacheKey);
            
            await _questionRepository.InsertAsync(question);
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            _memoryCache.Remove(QuestionDefaults.QuestionAllCacheKey);
            _memoryCache.Remove(QuestionDefaults.QuestionyIdCacheKey);
            
            await _questionRepository.UpdateAsync(question);
        }

        public async Task DeleteQuestionAsync(int questionID)
        {
            _memoryCache.Remove(QuestionDefaults.QuestionAllCacheKey);
            _memoryCache.Remove(QuestionDefaults.QuestionyIdCacheKey);
            
            await _questionRepository.DeleteAsync(questionID);
        }
        
        #endregion
    }
}