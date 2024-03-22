using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizData;

namespace QuizRepository
{

    public interface IDbContext
    {
        #region Methods

        DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        #endregion
    }
}