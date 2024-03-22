using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizData;


namespace QuizRepository
{
    public class QuizDBContext : DbContext, IDbContext
    {
        #region Ctor

        public QuizDBContext(DbContextOptions<QuizDBContext> options) : base(options)
        {
            
        }

        #endregion

        #region Methods

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase
        {
            return base.Set<TEntity>();
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }



        #endregion

        #region tables

        public DbSet<Quiz> Quizes { get; set; }
        
        public DbSet<Question> Questions { get; set; }
        
        public DbSet<Answer> Answers { get; set; }
        
        public DbSet<QuizTheme> QuizThemes { get; set; }
        
        public DbSet<QuestionType> QuestionTypes { get; set; }
        
        public DbSet<AnswerType> AnswerTypes { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Right> Rights { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }
        
        public DbSet<UserRight> UserRights { get; set; }
        
        public DbSet<RoleRight> RoleRights { get; set; }
        
        public DbSet<Image> Images { get; set; }

        public DbSet<ExamType> ExamTypes { get; set; } 

        #endregion

        #region override

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Quiz>().HasMany(p => p.QuizThemes).WithOne(p => p.Quiz).IsRequired();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        #endregion
    }
}