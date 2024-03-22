using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;
using QuizData;


namespace QuizRepository
{
    public interface IElasticSearchRepository<TEntity> where TEntity : EntityBase
    {
        #region Async Methods

        Task<ISearchResponse<TEntity>> SearchAsync(string query);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        
        Task DeleteAll();
        
        #endregion
    }
}