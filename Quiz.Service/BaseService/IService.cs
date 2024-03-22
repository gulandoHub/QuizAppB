using System.Collections.Generic;
using System.Threading.Tasks;
using QuizData;


namespace QuizService
{
    public interface IService<TEntity> where TEntity : EntityBase
    {
        #region Methods

        TEntity GetById(object id);
        
        List<TEntity> GetAll();
        
        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        void Delete(int id);
        
        #endregion
        
        #region async Methods

        Task<TEntity> GetByIdAsync(object id);

        Task<List<TEntity>> GetAllAsync();
        
        Task InsertAsync(TEntity entity);

        Task InsertAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);

        Task UpdateAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(int id);
        
        #endregion
    }
}