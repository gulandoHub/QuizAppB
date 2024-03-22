using System.Threading.Tasks;
using Nest;
using QuizData;


namespace QuizRepository
{
    public class ElasticSearchRepository<TEntity> : IElasticSearchRepository<TEntity> where TEntity : EntityBase
    {
        #region Fields

        private IElasticClient _elasticClient;
        
        #endregion

        #region Ctor
        
        public ElasticSearchRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        #endregion

        #region methods
        
        public async Task<ISearchResponse<TEntity>> SearchAsync(string query)
        {
            return  _elasticClient.SearchAsync<TEntity>(s => s.Query(q => q.QueryString(d => d.Query(query)))).Result;
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _elasticClient.IndexDocumentAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _elasticClient.UpdateAsync<TEntity>(entity, u => u.Doc(entity));
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _elasticClient.DeleteAsync<TEntity>(entity);
        }

        public async Task DeleteAll()
        {
            await _elasticClient.DeleteByQueryAsync<TEntity>(q => q.MatchAll());
        }
        
        #endregion
    }
}