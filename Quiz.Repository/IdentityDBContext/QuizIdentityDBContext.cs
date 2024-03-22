using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizData;


namespace QuizRepository
{
    public class QuizIdentityDBContext : IdentityDbContext<AppUser>,IIdentityDBContext
    {
        public QuizIdentityDBContext(DbContextOptions<QuizIdentityDBContext> options) : base(options) { }
    }
}
