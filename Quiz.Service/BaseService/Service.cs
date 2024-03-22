using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizData;
using QuizRepository;

namespace QuizService
{
    public class Service<TEntity> : IService<TEntity> where TEntity : EntityBase
    {
        #region Fields

        private readonly IRepository<TEntity> _repository;

        #endregion

        #region ctor
        
        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        
        #endregion
        
        #region methods
        
        public virtual TEntity GetById(object id)
        {
            return _repository.GetById(id);
        }

        public virtual List<TEntity> GetAll()
        {
            return _repository.Table.ToList();
        }

        public virtual void Insert(TEntity entity)
        {
            _repository.Insert(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            _repository.Insert(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            _repository.Update(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            _repository.Delete(entities);
        }

        public virtual void Delete(int id)
        {
            _repository.Delete(id);
        }

        #endregion
        
        #region async methods
        
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _repository.Table.ToListAsync();
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await _repository.InsertAsync(entity);
        }

        public virtual async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await _repository.InsertAsync(entities);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            await _repository.UpdateAsync(entities);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public virtual async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            await _repository.DeleteAsync(entities);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        
        #endregion
    }
}