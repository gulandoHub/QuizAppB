using System.Threading.Tasks;
using Nest;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class SearchService<TEntity> : ISearchService<TEntity> where TEntity : EntityBase
    {
        #region properties

        private readonly IElasticSearchRepository<TEntity> _searchRepository;

        #endregion
        
        #region ctor
        
        public SearchService(IElasticSearchRepository<TEntity> searchRepository)
        {
            _searchRepository = searchRepository;
        }

        #endregion
        
        #region methods
        
        public async Task<ISearchResponse<TEntity>> SearchByQery(string query)
        {
            return await _searchRepository.SearchAsync(query);
        }

        public async Task AddEntityAsync(TEntity entity)
        {
            await _searchRepository.InsertAsync(entity);
        }

        public async Task UpdatEntityAsync(TEntity entity)
        {
            await _searchRepository.UpdateAsync(entity);
        }

        public async Task DeleteEntityAsync(TEntity entity)
        {
            await _searchRepository.DeleteAsync(entity);
        }
        
        #endregion
    }
}