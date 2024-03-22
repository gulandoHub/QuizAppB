using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class QuizThemeService : IQuizThemeService
    {
        #region properties
        
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<QuizTheme> _quizThemeRepository;
        
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public QuizThemeService(IRepository<Quiz> quizRepository, IRepository<QuizTheme> quizThemeRepository,IMemoryCache memoryCache)
        {
            _quizRepository = quizRepository;
            _quizThemeRepository = quizThemeRepository;
            
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods
        
        public List<QuizTheme> GetAllQuizThemes()
        {
            if (_memoryCache.TryGetValue(QuizThemeDefaults.QuizThemeAllCacheKey, out List<QuizTheme> quizThemes)) 
                return quizThemes.ToList();
                
            quizThemes = _quizThemeRepository.Table.OrderBy(k => k.QuizID).ToList();
            _memoryCache.Set(QuizThemeDefaults.QuizThemeAllCacheKey, quizThemes);

            return quizThemes.ToList();
        }

        public List<QuizThemeSummary> GetQuizThemeSummary(int quizThemeID = 0)
        {
            var result = (from quizes in _quizRepository.Table
                join quizThemes in _quizThemeRepository.Table on quizes.ID equals quizThemes.QuizID
                where quizThemes.ID == quizThemeID || quizThemeID == 0
                select new QuizThemeSummary
                {
                    QuizID = quizes.ID,
                    ID = quizThemes.ID,
                    QuizName = quizes.QuizName,
                    QuizThemeName = quizThemes.QuizThemeName
                }).OrderBy(k => k.QuizName).ToList();

            return result;     
        }
        
        public QuizTheme GetQuizThemeByID(int quizThemeID)
        {
            return _quizThemeRepository.GetById(quizThemeID);
        }

        public void UpdateQuizTheme(QuizTheme quizTheme)
        {
            _memoryCache.Remove(QuizThemeDefaults.QuizThemeAllCacheKey);
            _memoryCache.Remove(QuizThemeDefaults.QuizThemeIdCacheKey);
            
            _quizThemeRepository.Update(quizTheme);
        }

        public void AddQuizTheme(QuizTheme quizTheme)
        {
            _memoryCache.Remove(QuizThemeDefaults.QuizThemeAllCacheKey);
            _memoryCache.Remove(QuizThemeDefaults.QuizThemeIdCacheKey);
            
            _quizThemeRepository.Insert(quizTheme);
        }

        public void DeleteQuizTheme(int quizThemeID)
        {
            _memoryCache.Remove(QuizThemeDefaults.QuizThemeAllCacheKey);
            _memoryCache.Remove(QuizThemeDefaults.QuizThemeIdCacheKey);
            
            _quizThemeRepository.Delete(quizThemeID);
        }

        #endregion
        
        #region async methods
        
        public async Task<List<QuizTheme>> GetAllQuizThemesAsync()
        {
            if (_memoryCache.TryGetValue(QuizThemeDefaults.QuizThemeAllCacheKey, out List<QuizTheme> quizThemes)) 
                return quizThemes.ToList();
                
            quizThemes = await _quizThemeRepository.Table.OrderBy(k => k.QuizID).ToListAsync();
            _memoryCache.Set(QuizThemeDefaults.QuizThemeAllCacheKey, quizThemes);

            return quizThemes.ToList();
        }

        public async Task<List<QuizThemeSummary>> GetQuizThemeSummaryAsync(int quizThemeID = 0)
        {
            var result = (from quizes in _quizRepository.Table
                join quizThemes in _quizThemeRepository.Table on quizes.ID equals quizThemes.QuizID
                where quizThemes.ID == quizThemeID || quizThemeID == 0
                select new QuizThemeSummary
                {
                    QuizID = quizes.ID,
                    ID = quizThemes.ID,
                    QuizName = quizes.QuizName,
                    QuizThemeName = quizThemes.QuizThemeName
                }).OrderBy(k => k.QuizName).ToListAsync();

            return await result;  
        }

        public async Task<QuizTheme> GetQuizThemeByIDAsync(int quizThemeID)
        {
            return await _quizThemeRepository.GetByIdAsync(quizThemeID);
        }

        public async Task AddQuizThemeAsync(QuizTheme quizTheme)
        {
            _memoryCache.Remove(QuizThemeDefaults.QuizThemeAllCacheKey);
            _memoryCache.Remove(QuizThemeDefaults.QuizThemeIdCacheKey);
            
            await _quizThemeRepository.InsertAsync(quizTheme);
        }

        public async Task UpdateQuizThemeAsync(QuizTheme quizTheme)
        {
            _memoryCache.Remove(QuizThemeDefaults.QuizThemeAllCacheKey);
            _memoryCache.Remove(QuizThemeDefaults.QuizThemeIdCacheKey);
            
            await _quizThemeRepository.UpdateAsync(quizTheme);
        }

        public async Task DeleteQuizThemeAsync(int quizThemeID)
        {
            _memoryCache.Remove(QuizThemeDefaults.QuizThemeAllCacheKey);
            _memoryCache.Remove(QuizThemeDefaults.QuizThemeIdCacheKey);
            
            await _quizThemeRepository.DeleteAsync(quizThemeID);
        }
        
        #endregion
    }
}