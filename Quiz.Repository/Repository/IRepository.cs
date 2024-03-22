using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizData;


namespace QuizRepository
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        #region Methods

        TEntity GetById(object id);

        void Insert(TEntity entity);

        int InsertGetID(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        void Delete(int id);
        
        #endregion

        #region Properties

        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableNoTracking { get; }

        #endregion
        
        #region Async Methods

        Task<TEntity> GetByIdAsync(object id);

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