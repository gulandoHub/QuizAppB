using System.Threading.Tasks;
using Nest;
using QuizData;


namespace QuizService
{
    public interface ISearchService<TEntity> where TEntity : EntityBase 
    {
        #region methods
        
        #endregion
        
        #region async methods
        
        Task<ISearchResponse<TEntity>> SearchByQery(string query);

        Task AddEntityAsync(TEntity entity);
        
        Task UpdatEntityAsync(TEntity entity);

        Task DeleteEntityAsync(TEntity entity);
        
        #endregion
    }
}